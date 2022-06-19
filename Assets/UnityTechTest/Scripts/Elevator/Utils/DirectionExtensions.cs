using System;
using UnityTechTest.Scripts.Elevator.Model;

namespace UnityTechTest.Scripts.Elevator.Utils
{
    public static class DirectionExtensions
    {
        /// <summary>
        /// Reverse direction (to opposite side). Up <=> Down, Stationary remains as-is.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Direction Reverse(this Direction direction)
        {
            return direction switch
            {
                Direction.Stationary => Direction.Stationary,
                Direction.Up         => Direction.Down,
                Direction.Down       => Direction.Up,
                Direction.None       => Direction.None,

                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}