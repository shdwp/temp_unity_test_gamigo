using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityTechTest.Scripts.Maze.GraphLib.Utils
{
    /// <summary>
    /// Implementation of the BFS graph path algorithm
    /// </summary>
    public static class GraphBfs
    {
        /// <summary>
        /// Check if path exists from startingNode to another node that satisfies the predicate,
        /// using breadth-first graph traversal.
        /// </summary>
        /// <param name="startingNode"></param>
        /// <param name="pred"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool CheckIfPathExists<T>(T startingNode, Predicate<T> pred) where T: IGraphNode<T>
        {
            if (pred(startingNode))
            {
                return true;
            }
            
            var visited = new HashSet<T> { startingNode };
            var queue = new Queue<T>();
            queue.Enqueue(startingNode);

            while (queue.Any())
            {
                var node = queue.Dequeue();
                foreach (var connectedNode in node)
                {
                    if (pred(connectedNode))
                    {
                        return true;
                    }
                    
                    if (!visited.Contains(connectedNode))
                    {
                        visited.Add(connectedNode);
                        queue.Enqueue(connectedNode);
                    }
                }
            }

            return false;
        }
    }
}