using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Utils
{
    public static class GraphBfs
    {
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