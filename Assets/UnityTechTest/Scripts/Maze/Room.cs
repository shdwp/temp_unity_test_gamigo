using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityTechTest.Scripts.Maze.GraphLib;
using UnityTechTest.Scripts.Maze.GraphLib.Utils;

namespace UnityTechTest.Scripts.Maze
{

    /// <summary>
    /// Room class, a graph node that has 4 connections to adjacent rooms.
    /// Connections may or may not be bi-directional.
    ///
    /// NOTE: IPathable either limits Room to also being a node class,
    /// or enforces reverse connection between Room and its container node.
    /// Normally there would be a GraphNode<Room>, and also a
    /// general Graph<Room> to keep the meta-information.
    /// </summary>
    public class Room : IGraphNode<Room>, IPathable
    {
        /// <summary>
        /// Name of the room
        /// </summary>
        public string Name { get; }

        [CanBeNull] public Room North { get; private set; }
        [CanBeNull] public Room South { get; private set; }
        [CanBeNull] public Room East  { get; private set; }
        [CanBeNull] public Room West  { get; private set; }

        public Room(string name)
        {
            Name = name;
        }

        private readonly HashSet<Room> _connectedNodes = new();

        /// <summary>
        /// Set one of the paths to uni-directional connection to other room.
        /// If path on that direction already exists this replaces it.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="room"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetUnidirectionalPath(RoomPathDirection dir, Room room)
        {
            switch (dir)
            {
                case RoomPathDirection.North:
                    SwapConnection(North, room);
                    North = room;
                    break;

                case RoomPathDirection.South:
                    SwapConnection(South, room);
                    South = room;
                    break;

                case RoomPathDirection.East:
                    SwapConnection(East, room);
                    East = room;
                    break;

                case RoomPathDirection.West:
                    SwapConnection(West, room);
                    West = room;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), $"Unknown direction: {dir}");
            }
        }

        /// <summary>
        /// Connect this room to another with bi-directional connection. Directions can be specified separately.
        /// If path on that direction already exists this replaces it.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="room"></param>
        public void SetBidirectionalPath(RoomPathDirection to, RoomPathDirection from, Room room)
        {
            SetUnidirectionalPath(to, room);
            room.SetUnidirectionalPath(from, this);
        }

        /// <summary>
        /// Connect this room to another with bi-directional connection. Returning path direction will be mirrored.
        /// If path on that direction already exists this replaces it.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="room"></param>
        public void SetBidirectionalPath(RoomPathDirection dir, Room room)
        {
            SetBidirectionalPath(dir, dir.Reverse(), room);
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