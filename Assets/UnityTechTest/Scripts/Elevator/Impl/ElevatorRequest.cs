using System;
using UnityTechTest.Scripts.Elevator.Model;

namespace UnityTechTest.Scripts.Elevator.Impl
{
    /// <summary>
    /// Elevator request struct.
    /// </summary>
    public struct ElevatorRequest
    {
        /// <summary>
        /// Type of the request. Destination type will have direction set to None
        /// </summary>
        public ElevatorRequestType type;

        /// <summary>
        /// Target floor
        /// </summary>
        public int floor;

        /// <summary>
        /// Target direction (if type is Summon)
        /// </summary>
        public Direction direction;

        public ElevatorRequest(ElevatorRequestType type, int floor, Direction direction = Direction.None)
        {
            if (type == ElevatorRequestType.Summon && direction == Direction.None)
            {
                throw new ArgumentException("Direction must be specified for Summon type", nameof(direction));
            }

            this.type = type;
            this.floor = floor;
            this.direction = direction;
        }

        /// <summary>
        /// Check if request is serviced by elevator at specified floor, travelling at specified direction.
        ///
        /// If elevator is parked (Stationary) then it can always be serviced
        /// (since elevator will immediately go where user asks it to)
        /// </summary>
        /// <param name="elevatorFloor"></param>
        /// <param name="elevatorDirection"></param>
        /// <returns></returns>
        public bool CanBeServicedBy(int elevatorFloor, Direction elevatorDirection)
        {
            return elevatorFloor == floor && (type == ElevatorRequestType.Destination || elevatorDirection == direction || elevatorDirection == Direction.Stationary);
        }

        /// <summary>
        /// Check if request is in the direction of travel relative to the specified floor.
        ///
        /// For example, if we're at floor 5 travelling upwards, then 6 would be in the direction of travel, but 4 wouldn't
        /// </summary>
        /// <param name="elevatorFloor"></param>
        /// <param name="elevatorDirection"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool IsInDirectionOfTravel(int elevatorFloor, Direction elevatorDirection)
        {
            switch (elevatorDirection)
            {
                case Direction.Up:
                    return floor > elevatorFloor;

                case Direction.Down:
                    return floor < elevatorFloor;

                case Direction.Stationary:
                    return true;

                default:
                    throw new ArgumentOutOfRangeException(nameof(elevatorDirection), elevatorDirection, null);
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()} (type {type}, floor {floor}, dir {direction})";
        }
    }
}