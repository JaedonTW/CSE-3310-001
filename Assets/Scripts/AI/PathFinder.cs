using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    static class PathFinder
    {
        class Node
        {
            public Node(Vector2Int v)
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
            public float cost;
            public int x;
            public int y;
            public static bool operator <(Node a, Node b) => a.heuristicCost < b.heuristicCost;
            public static bool operator >(Node a, Node b) => a.heuristicCost > b.heuristicCost;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="pathMap">An element is set to true when it has a clear path</param>
        /// <returns>a list of intermediate locations in reverse order, or null if no path exists</returns>
        public static Vector2Int[] GeneratePath(Vector2Int start, Vector2Int end, bool[,] pathMap)
        {
            var fringe = new List<Node>((int)(start - end).magnitude);
            bool[,] crossedMap = new bool[pathMap.GetLength(0), pathMap.GetLength(1)];
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
                crossedMap[node.x, node.y] = true;
                // getting new possible nodes
                for (int x = Mathf.Max(0, node.x - 1); x < Mathf.Min(pathMap.GetLength(0), node.x + 2); x++)
                    for (int y = Mathf.Max(0, node.y - 1); y < Mathf.Min(pathMap.GetLength(1), node.y + 2); y++)
                        if(!crossedMap[x,y] && pathMap[x,y])
                        {
                            float cost;
                            if (x == node.x || y == node.y)
                                cost = 1;
                            // making sure we will not run into any corners.
                            else if (pathMap[node.x, y] && pathMap[x, node.y])
                                cost = diagonalCost;
                            else continue;
                            cost += node.cost;
                            var newNode = new Node(
                                x, y,
                                node,
                                cost,
                                Mathf.Sqrt(Sqr(x - end.x) + Sqr(y - end.y))
                                );
                        }
            }
            return null;
        }
        private static float Sqr(float n) => n * n;
    }
}
