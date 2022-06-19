using System.Collections.Generic;
using UnityTechTest.Scripts.Elevator.Interfaces;
using UnityTechTest.Scripts.Elevator.Model;
using UnityTechTest.Scripts.Elevator.Utils;

namespace UnityTechTest.Scripts.Elevator.Impl
{
    /// <summary>
    /// Scheduler implementation based on modified LOOK scheduling algorithm.
    /// 1. When elevator is parked, will go to the closest requested floor.
    /// 2. Then it will continue travelling into previous direction to the next closest request.
    /// 3. If there are no requests in that direction will go to the next closest request in the opposite direction.
    /// </summary>
    public sealed class ElevatorScheduler : IElevatorScheduler
    {
        private readonly List<ElevatorRequest> _requests = new();

        public void AddRequest(ElevatorRequest request)
        {
            if (!_requests.Contains(request))
            {
                _requests.Add(request);
            }
        }

        public int? NextTargetFloor(int currentFloor, Direction direction, bool stoppedOnFloor, List<ElevatorRequest> servicedList)
        {
            if (stoppedOnFloor)
            {
                // remove requests that were just serviced (on the current floor)
                servicedList.AddRange(GetAndRemoveServicedRequests(currentFloor, direction));
            }

            // search for the next closest request (that can be serviced) in the current direction of travel
            // when there are not serviceable requests in the current direction, reverse the direction and repeat
            return ServiceRequestsAndFindNextTarget(currentFloor, direction, stoppedOnFloor, servicedList) ??
                   ServiceRequestsAndFindNextTarget(currentFloor, direction.Reverse(), stoppedOnFloor, servicedList);
        }

        private int? ServiceRequestsAndFindNextTarget(int floor, Direction direction, bool stoppedOnFloor, List<ElevatorRequest> servicedList)
        {
            if (stoppedOnFloor)
            {
                // remove requests that were just serviced (on the current floor)
                servicedList.AddRange(GetAndRemoveServicedRequests(floor, direction));
            }

            var elevatorStationary = direction == Direction.Stationary;

            // target floor in the direction of travel
            int? directionOfTravelTarget = null;

            // if no such floor is found, then find 
            int? reverseTarget = null;

            foreach (var req in _requests)
            {
                var inDirectionOfTravel = req.IsInDirectionOfTravel(floor, direction);

                if (elevatorStationary || inDirectionOfTravel)
                {
                    // if request in the same direction can be serviced it will always be a priority
                    if (req.CanBeServicedBy(req.floor, direction))
                    {
                        directionOfTravelTarget = ClosestFloor(directionOfTravelTarget, req.floor, direction);
                    }
                    
                    // when there are no same-direction serviceable requests,
                    // select one that will be used to turn-around point (since it can be serviced that way)
                    else if (req.CanBeServicedBy(req.floor, direction.Reverse()))
                    {
                        reverseTarget = ClosestFloor(reverseTarget, req.floor, direction.Reverse());
                    }
                }
            }

            return directionOfTravelTarget ?? reverseTarget;
        }

        private IEnumerable<ElevatorRequest> GetAndRemoveServicedRequests(int floor, Direction direction)
        {
            for (var reqIdx = 0; reqIdx < _requests.Count; reqIdx++)
            {
                var req = _requests[reqIdx];
                if (req.CanBeServicedBy(floor, direction))
                {
                    _requests.RemoveAt(reqIdx);
                    reqIdx--;

                    yield return req;
                }
            }
        }

        private static int? ClosestFloor(int? current, int floor, Direction direction)
        {
            if (direction == Direction.Up)
            {
                if ((current ?? int.MaxValue) > floor)
                {
                    return floor;
                }
            }
            else
            {
                if ((current ?? int.MinValue) < floor)
                {
                    return floor;
                }
            }

            return current;
        }
    }
}