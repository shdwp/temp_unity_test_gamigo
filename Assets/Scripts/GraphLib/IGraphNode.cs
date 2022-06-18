using System.Collections.Generic;

namespace GraphLib
{
    // Normally this would be a graph data-container (encapsulating Room)
    // but since I'm bound to comply to IPathable interface, this is not really viable
    // henceforth simple interface to iterate over node connections
    public interface IGraphNode<out T>: IEnumerable<T> where T: IGraphNode<T>
    {
        
    }
}