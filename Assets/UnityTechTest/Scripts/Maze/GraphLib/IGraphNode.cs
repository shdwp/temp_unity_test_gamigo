using System.Collections.Generic;

namespace UnityTechTest.Scripts.Maze.GraphLib
{
    /// <summary>
    /// Generic graph node interface. Enumerates over its connections to other nodes.
    /// 
    /// NOTE:
    /// Normally this would be a graph data-container (encapsulating Room)
    /// but since I'm bound to comply to IPathable interface, this is not really viable
    /// henceforth simple interface to iterate over node connections
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGraphNode<out T>: IEnumerable<T> where T: IGraphNode<T>
    {
        
    }
}