using System;
using System.Collections;
using System.Collections.Generic;
using GraphLib;
using GraphLib.Utils;

namespace Maze
{
    /**
     * IPathable either limits Room to also being a node class,
     * or enforces reverse connection between Room and its container node.
     * Normally there would be a GraphNode<Room>, and also a
     * general Graph<Room> to keep the meta-information.
     */
    public class Room : IGraphNode<Room>, IPathable
    {
        public string Name { get; }

        public Room North { get; private set; }
        public Room South { get; private set; }
        public Room East  { get; private set; }
        public Room West  { get; private set; }

        public Room(string name)
        {
            Name = name;
        }

        private readonly HashSet<Room> _connectedNodes = new();

        public void SetUnidirectionalPath(GraphPathDirection dir, Room room)
        {
            switch (dir)
            {
                case GraphPathDirection.North:
                    SwapConnection(North, room);
                    North = room;
                    break;

                case GraphPathDirection.South:
                    SwapConnection(South, room);
                    South = room;
                    break;

                case GraphPathDirection.East:
                    SwapConnection(East, room);
                    East = room;
                    break;

                case GraphPathDirection.West:
                    SwapConnection(West, room);
                    West = room;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), $"Unknown direction: {dir}");
            }
        }

        public void SetBidirectionalPath(GraphPathDirection to, GraphPathDirection from, Room room)
        {
            SetUnidirectionalPath(to, room);
            room.SetUnidirectionalPath(from, this);
        }

        public void SetBidirectionalPath(GraphPathDirection dir, Room room)
        {
            SetBidirectionalPath(dir, dir.GetOppositeDirection(), room);
        }
        
        /*
         * IGraphNode
         */
        
        public IEnumerator<Room> GetEnumerator()
        {
            return _connectedNodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        /*
         * IPathable
         */
        
        public bool PathExistsTo(string endingRoomName)
        {
            return GraphBfs.CheckIfPathExists(this, r => r.Name == endingRoomName);
        }

        /*
         * Private
         */
        
        private void SwapConnection(Room oldConnection, Room newConnection)
        {
            _connectedNodes.Remove(oldConnection);
            _connectedNodes.Add(newConnection);
        }
    }
}