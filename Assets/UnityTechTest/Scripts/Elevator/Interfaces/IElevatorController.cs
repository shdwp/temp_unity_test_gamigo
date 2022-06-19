using System;
using UnityTechTest.Scripts.Elevator.Model;
using UnityTechTest.Scripts.Elevator.Utils;

namespace UnityTechTest.Scripts.Elevator.Interfaces
{
    /// <summary>
    /// Controller interface for the elevator.
    ///
    /// Provides methods to control elevator scheduling.
    /// </summary>
    public interface IElevatorController : IContextService
    {
        /// <summary>
        /// Fired when stopping on a summoned floor (floor up/down buttons).
        /// </summary>
        event Action<int, Direction> ReachedSummonedFloor;
        
        /// <summary>
        /// Fired when stopping on a destination floor (elevator buttons) 
        /// </summary>
        event Action<int>            ReachedDestinationFloor;

        /// <summary>
        /// Summon elevator coming in the specified direction to the specified floor.
        /// Normally this comes from the floor mounted buttons (to pick up passengers).
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="direction"></param>
        void SummonButtonPushed(int floor, Direction direction);
        
        /// <summary>
        /// Request elevator to travel to the specified floor.
        /// Normally this comes from the elevator mounted buttons (to drop off passengers).
        /// </summary>
        /// <param name="floor"></param>
        void FloorButtonPushed(int floor);
    }
}