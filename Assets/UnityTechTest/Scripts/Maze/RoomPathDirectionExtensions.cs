using System;

namespace UnityTechTest.Scripts.Maze
{
    public static class RoomPathDirectionExtensions
    {
        /// <summary>
        /// Reverse direction (returns opposite direction, N-S and E-W)
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static RoomPathDirection Reverse(this RoomPathDirection dir)
        {
            return dir switch
            {
                RoomPathDirection.North => RoomPathDirection.South,
                RoomPathDirection.South => RoomPathDirection.North,
                RoomPathDirection.East  => RoomPathDirection.West,
                RoomPathDirection.West  => RoomPathDirection.East,
                _ => throw new ArgumentOutOfRangeException(nameof(dir), $"Unknown direction: {dir}")
            };
        }
    }
}