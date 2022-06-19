using System;
using System.Collections.Generic;
using UnityTechTest.Scripts.Elevator.Interfaces;
using UnityTechTest.Scripts.Elevator.Model;

namespace UnityTechTest.Scripts.Elevator.Impl
{
    /// <summary>
    /// Elevator controller using injected motor and scheduler.
    ///
    /// Scheduling is updated (asking for a new floor) each time:
    /// 1. A target floor is reached
    /// 2. User presses any of the buttons (to kickstart the movement when parked,
    /// or potentially redirect the target floor to new requested floor that is closer than the last)
    /// </summary>
    public sealed class ElevatorController : IElevatorController
    {
        private readonly IElevatorMotor     _motor;
        private readonly IElevatorScheduler _scheduler;

        public event Action<int, Direction> ReachedSummonedFloor;
        public event Action<int>            ReachedDestinationFloor;

        public ElevatorController(IElevatorMotor motor, IElevatorScheduler scheduler)
        {
            _motor = motor;
            _scheduler = scheduler;
        }

        public void Start()
        {
            _motor.ReachedFloor += OnMotorReachedFloor;
        }

        public void Stop()
        {
            _motor.ReachedFloor -= OnMotorReachedFloor;
        }

        public void SummonButtonPushed(int floor, Direction direction)
        {
            var request = new ElevatorRequest
            {
                type = ElevatorRequestType.Summon,
                floor = floor,
                direction = direction,
            };

            _scheduler.AddRequest(request);
            SchedulingUpdate();
        }

        public void FloorButtonPushed(int floor)
        {
            var request = new ElevatorRequest
            {
                type = ElevatorRequestType.Destination,
                floor = floor,
            };

            _scheduler.AddRequest(request);
            SchedulingUpdate();
        }

        private void OnMotorReachedFloor(int _)
        {
            SchedulingUpdate();
        }

        private void SchedulingUpdate()
        {
            var justServiced = new List<ElevatorRequest>();
            var newTarget = _scheduler.NextTargetFloor(_motor.CurrentFloor, _motor.CurrentDirection, _motor.StoppedOnAFloor, justServiced);
            foreach (var request in justServiced)
            {
                switch (request.type)
                {
                    case ElevatorRequestType.Destination:
                        ReachedDestinationFloor?.Invoke(request.floor);
                        break;

                    case ElevatorRequestType.Summon:
                        ReachedSummonedFloor?.Invoke(request.floor, request.direction);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (newTarget.HasValue)
            {
                _motor.GoToFloor(newTarget.Value);
            }
            else
            {
                _motor.Park();
            }
        }
    }
}