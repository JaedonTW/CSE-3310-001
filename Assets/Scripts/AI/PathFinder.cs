using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.AI
{
    static class PathFinder
    {
        class Node
        {
            /// <summary>
            /// Creates a node object.
            /// </summary>
            /// <param name="posX">Horizontal position</param>
            /// <param name="posY">Virtical Position</param>
            public Node(int posX, int posY)
            {
                x = posX;
                y = posY;
            }
            /// <summary>
            /// Creates a node object.
            /// </summary>
            /// <param name="v">The vector to be used for placement</param>
            public Node(Vector3Int v)
            {
                x = v.x;
                y = v.y;
                cost = heuristicCost = 0;
                prev = null;
            }
            /// <summary>
            /// Creates a node object.
            /// </summary>
            /// <param name="posX">horizontal position</param>
            /// <param name="posY">virtical position</param>
            /// <param name="prev">previous position</param>
            /// <param name="cost">cost to get to this node</param>
            /// <param name="heuristic">heuristic to get to the destination</param>
            public Node(int posX, int posY, Node prev, float cost, float heuristic)
            {
                x = posX;
                y = posY;
                this.prev = prev;
                this.cost = cost;
                heuristicCost = cost + heuristic;
            }
            /// <summary>
            /// Previous node to this one.
            /// </summary>
            public Node prev;
            float heuristicCost;
            /// <summary>
            /// The heuristic for this node.
            /// </summary>
            public float Heuristic { set { heuristicCost = value; } }
            /// <summary>
            /// Cost to get to this node.
            /// </summary>
            public float cost;
            /// <summary>
            /// Horizontal position of this node.
            /// </summary>
            public int x;
            /// <summary>
            /// Virtical position of this node.
            /// </summary>
            public int y;
            /// <summary>
            /// Compares the heuristic value between two nodes.
            /// </summary>
            /// <param name="a">First node</param>
            /// <param name="b">Second node</param>
            /// <returns></returns>
            public static bool operator <(Node a, Node b) => a.heuristicCost < b.heuristicCost;
            /// <summary>
            /// Compares the heuristic value between two nodes.
            /// </summary>
            /// <param name="a">First node</param>
            /// <param name="b">Second node</param>
            /// <returns></returns>
            public static bool operator >(Node a, Node b) => a.heuristicCost > b.heuristicCost;
            /// <summary>
            /// Compares the heuristic value between two nodes.
            /// </summary>
            /// <param name="a">First node</param>
            /// <param name="b">Second node</param>
            /// <returns></returns>
            public static bool operator ==(Node a, Node b)
            {
                if (ReferenceEquals(a, null))
                    return ReferenceEquals(b, null);
                if (ReferenceEquals(b, null))
                    return false;
                return a.x == b.x && a.y == b.y;
            }
            /// <summary>
            /// Compares the heuristic value between two nodes.
            /// </summary>
            /// <param name="a">First node</param>
            /// <param name="b">Second node</param>
            /// <returns></returns>
            public static bool operator !=(Node a, Node b)
            {
                if (ReferenceEquals(a, null))
                    return !ReferenceEquals(b, null);
                if (ReferenceEquals(b, null))
                    return true;
                return a.x != b.x || a.y != b.y;
            }
            /// <summary>
            /// Converts a node into a string representation.
            /// </summary>
            /// <returns>String representation for this node.</returns>
            public override string ToString()
            {
                return "< " + x + ", " + y + " >";
            }
        }
        /// <summary>
        /// Generates a path from 'start' to 'end' using Greedy search.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="pathMap">An element is set to true when it has a clear path</param>
        /// <returns>a list of intermediate locations in reverse order, or null if no path exists</returns>
        public static Vector2Int[] GeneratePath(Vector3Int start, Vector3Int end, Tilemap Walls)
        {
            // NOTE: greedy searched was used because for our maps this will typically be less computationally intensive then A*
            //   and only slightly less efficient as greedy is optimal when transitions costs are uniform and our transition 
            //   costs are almost uniform.
            // Making sure we are not trying to get somewhere inside of a wall or off of the map
            if (!Walls.cellBounds.Contains(end) || Walls.HasTile(end))
                return null;
            var fringe = new List<Node>((int)(start - end).magnitude);
            // an array to hold already visited locations, as repeat visits will be useless and should be auto discarded
            bool[,] crossedMap = new bool[Walls.cellBounds.xMax - Walls.cellBounds.xMin, Walls.cellBounds.yMax - Walls.cellBounds.yMin];
            fringe.Add(new Node(start));
            var diagonalCost = Mathf.Sqrt(2);
            Node node;
            while (fringe.Count > 0)
            {
                node = fringe[0];
                if (node.x == end.x && node.y == end.y)
                {
                    // construct array to end
                    int count = 0;
                    Node dupe = node;
                    while(dupe != null)
                    {
                        dupe = dupe.prev;
                        count++;
                    }
                    Vector2Int[] positions = new Vector2Int[count];
                    for(int i = 0; i < count; i++)
                    {
                        positions[i] = new Vector2Int(node.x, node.y);
                        node = node.prev;
                    }
                    return positions;
                }
                fringe.RemoveAt(0);
                if (crossedMap[node.x - Walls.cellBounds.xMin,node.y - Walls.cellBounds.yMin])
                    continue;
                crossedMap[node.x - Walls.cellBounds.xMin, node.y - Walls.cellBounds.yMin] = true;
                bool PathMap(int x, int y) => !Walls.HasTile(new Vector3Int(x, y, 0));
                // getting new possible nodes
                for (int x = node.x - 1; x <= node.x + 1; x++)
                    for (int y = node.y - 1; y <= node.y + 1; y++)
                    {
                        
                        if (!crossedMap[x-Walls.cellBounds.xMin,y - Walls.cellBounds.yMin] && PathMap(x, y))
                        {
                            var newNode = new Node(x, y);
                            float cost;
                            if (x == node.x || y == node.y)
                                cost = 1;
                            // making sure we will not run into any corners.
                            else if (PathMap(node.x, y) && PathMap(x, node.y))
                                cost = diagonalCost;
                            else continue;
                            cost += node.cost;
                            newNode.prev = node;
                            newNode.cost = cost;
                            newNode.Heuristic = Mathf.Sqrt(Sqr(x - end.x) + Sqr(y - end.y));
                            int i = 0;
                            for (; i < fringe.Count && newNode > fringe[i]; i++) ;
                            fringe.Insert(i, newNode);
                        }
                    }
            }
            return null;
        }
        private static float Sqr(float n) => n * n;
    }
}
