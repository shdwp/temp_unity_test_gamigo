using System;
using UnityTechTest.Scripts.Elevator.Model;
using UnityTechTest.Scripts.Elevator.Utils;

namespace UnityTechTest.Scripts.Elevator.Interfaces
{
    /// <summary>
    /// Motor interface, used to control elevator movement.
    ///
    /// TODO: doors open/close events should be added to correctly support lights
    /// </summary>
    public interface IElevatorMotor : IContextRuntimeService
    {
        /// <summary>
        /// Fired when elevator has changed it position.
        /// </summary>
        event Action<float> PositionUpdated;

        /// <summary>
        /// Fired when elevator stopped on a floor.
        /// </summary>
        event Action<int> ReachedFloor;

        /// <summary>
        /// Total amount of floors in elevator
        /// </summary>
        int AmountOfFloors { get; }

        /// <summary>
        /// Current floor
        /// </summary>
        int CurrentFloor { get; }

        /// <summary>
        /// Current travel direction. Goes to Stationary only if elevator is parked
        /// </summary>
        Direction CurrentDirection { get; }

        /// <summary>
        /// Whether elevator is currently stopped (servicing) a floor
        /// </summary>
        bool StoppedOnAFloor { get; }

        /// <summary>
        /// Start movement towards specified floor.
        /// Will skip all floors in-between, eventually stopping on the specified floor.
        /// Elevator will not be automatically parked.
        /// </summary>
        /// <param name="floor"></param>
        void GoToFloor(int floor);
        
        /// <summary>
        /// Park the elevator when there are no requests to service.
        /// Sets the CurrentDirection to Stationary.
        /// </summary>
        void Park();
    }
}