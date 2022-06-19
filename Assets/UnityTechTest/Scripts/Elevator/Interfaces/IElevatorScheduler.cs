using System.Collections.Generic;
using UnityTechTest.Scripts.Elevator.Impl;
using UnityTechTest.Scripts.Elevator.Model;

namespace UnityTechTest.Scripts.Elevator.Interfaces
{
    /// <summary>
    /// Scheduler interface.
    ///
    /// Accepts requests, providing API to get the next floor to move to.
    /// </summary>
    public interface IElevatorScheduler
    {
        /// <summary>
        /// Add user request model to the queue
        /// </summary>
        /// <param name="request"></param>
        void AddRequest(ElevatorRequest request);
        
        /// <summary>
        /// Get next floor to move to.
        ///
        /// Will populate servicedRequests list with requests that were just completed (if elevator is stopped).
        /// Can be called while elevator is moving to get new target if requests came during movement.
        /// </summary>
        /// <param name="currentFloor">Current floor the elevator is at</param>
        /// <param name="direction">Current travel direction</param>
        /// <param name="stoppedOnFloor">Whether elevator has currently stopped at a floor</param>
        /// <param name="servicedRequests">List instance to push serviced requests to</param>
        /// <returns></returns>
        int? NextTargetFloor(int currentFloor, Direction direction, bool stoppedOnFloor, List<ElevatorRequest> servicedRequests);
    }
}