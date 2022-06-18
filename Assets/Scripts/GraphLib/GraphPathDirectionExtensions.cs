using System;

namespace GraphLib
{
    public static class GraphPathDirectionExtensions
    {
        public static GraphPathDirection GetOppositeDirection(this GraphPathDirection dir)
        {
            return dir switch
            {
                GraphPathDirection.North => GraphPathDirection.South,
                GraphPathDirection.South => GraphPathDirection.North,
                GraphPathDirection.East  => GraphPathDirection.West,
                GraphPathDirection.West  => GraphPathDirection.East,
                _ => throw new ArgumentOutOfRangeException(nameof(dir), $"Unknown direction: {dir}")
            };
        }
    }
}