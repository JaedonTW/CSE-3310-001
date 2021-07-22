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
            public Node(int posX, int posY)
            {
                x = posX;
                y = posY;
            }
            public Node(Vector3Int v)
            {
                x = v.x;
                y = v.y;
                cost = heuristicCost = 0;
                prev = null;
            }
            public Node(int posX, int posY, Node prev, float cost, float heuristic)
            {
                x = posX;
                y = posY;
                this.prev = prev;
                this.cost = cost;
                heuristicCost = cost + heuristic;
            }
            public Node prev;
            float heuristicCost;
            public float Heuristic { set { heuristicCost = value; } }
            public float cost;
            public int x;
            public int y;
            public static bool operator <(Node a, Node b) => a.heuristicCost < b.heuristicCost;
            public static bool operator >(Node a, Node b) => a.heuristicCost > b.heuristicCost;
            public static bool operator ==(Node a, Node b)
            {
                if (ReferenceEquals(a, null))
                    return ReferenceEquals(b, null);
                if (ReferenceEquals(b, null))
                    return false;
                return a.x == b.x && a.y == b.y;
            }
            public static bool operator !=(Node a, Node b)
            {
                if (ReferenceEquals(a, null))
                    return !ReferenceEquals(b, null);
                if (ReferenceEquals(b, null))
                    return true;
                return a.x != b.x || a.y != b.y;
            }
            public override string ToString()
            {
                return "< " + x + ", " + y + " >";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="pathMap">An element is set to true when it has a clear path</param>
        /// <returns>a list of intermediate locations in reverse order, or null if no path exists</returns>
        public static Vector2Int[] GeneratePath(Vector3Int start, Vector3Int end, Tilemap Walls)
        {
            if (!Walls.cellBounds.Contains(end) || Walls.HasTile(end))
                return null;
            var fringe = new List<Node>((int)(start - end).magnitude);
            bool[,] crossedMap = new bool[Walls.cellBounds.xMax - Walls.cellBounds.xMin, Walls.cellBounds.yMax - Walls.cellBounds.yMin];
            fringe.Add(new Node(start));
            var diagonalCost = Mathf.Sqrt(2);
            Node node;
            while (fringe.Count > 0)
            {
                MonoBehaviour.print("fringe length: " + fringe.Count);
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
                    MonoBehaviour.print("Path found, length = " + count);
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
                {
                    MonoBehaviour.print("We already have: " + node);
                    continue;
                }
                crossedMap[node.x - Walls.cellBounds.xMin, node.y - Walls.cellBounds.yMin] = true;
                bool PathMap(int x, int y) => !Walls.HasTile(new Vector3Int(x, y, 0));
                /*
                bool PathMap(int x, int y)
                {
                    /*
                    for (int i = Walls.cellBounds.xMin; i <= Walls.cellBounds.xMax; i++)
                        for (int j = Walls.cellBounds.yMin; j <= Walls.cellBounds.yMax; j++)
                            if (Walls.HasTile(new Vector3Int(i, j, 0)))
                                throw null;
                                *//*
                    if(Walls.HasTile(new Vector3Int(x, y, 0)))
                    {
                        MonoBehaviour.print("Wall found!");
                        return false;
                    }
                    return true;
                }
                */
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
                            MonoBehaviour.print("Adding to fringe.");
                            fringe.Insert(i, newNode);
                        }
                    }
                MonoBehaviour.print("END");
            }
            return null;
        }
        private static float Sqr(float n) => n * n;
    }
}
