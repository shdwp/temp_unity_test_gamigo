using System;
using UnityEngine;
using UnityTechTest.Scripts.Elevator.Interfaces;
using UnityTechTest.Scripts.Elevator.Model;

namespace UnityTechTest.Scripts.Elevator.Impl
{
    /// <summary>
    /// Motor implementation
    /// </summary>
    public sealed class ElevatorMotor : IElevatorMotor
    {
        public event Action<float> PositionUpdated;
        public event Action<int>   ReachedFloor;

        public int       CurrentFloor     { get; private set; }
        public Direction CurrentDirection { get; private set; }
        public int       AmountOfFloors   { get; private set; }
        public bool      StoppedOnAFloor  => Time.realtimeSinceStartup <= _stoppedUntil;

        /// <summary>
        /// Error margin for travel. If distance between floor and elevator is less than
        /// this, elevator will be considered "on the floor"
        /// </summary>
        private const float ErrorMargin = 0.02f;

        private readonly float _speed;
        private readonly float _stopDuration;
        private          int   _targetFloor;
        private          float _currentFloor;
        private          float _stoppedUntil;

        public ElevatorMotor(float speed, float stopDuration, int amountOfFloors)
        {
            _speed = speed;
            _stopDuration = stopDuration;
            AmountOfFloors = amountOfFloors;
            CurrentDirection = Direction.Stationary;
        }

        public void Update()
        {
            if (StoppedOnAFloor)
            {
                // no movement if elevator is currently stopped
                return;
            }

            if (CurrentDirection == Direction.Stationary)
            {
                // no movement if elevator is parked
                return;
            }

            var floorDeltaAbs = Mathf.Abs(_targetFloor - _currentFloor);
            var speed = Mathf.Min(_speed * Time.deltaTime, floorDeltaAbs);
            if (floorDeltaAbs > ErrorMargin)
            {
                // distance to target floor is higher then margin, therefore move the elevator
                // in the specified direction
                _currentFloor += CurrentDirection == Direction.Up ? speed : -speed;
                PositionUpdated?.Invoke(_currentFloor);
            }
            else
            {
                // distance is lower, stop the elevator for the duration of the stop
                _stoppedUntil = Time.realtimeSinceStartup + _stopDuration;

                // and set/notify about the stop
                var currentFloor = Mathf.RoundToInt(_currentFloor);
                if (CurrentFloor != currentFloor)
                {
                    CurrentFloor = currentFloor;
                    ReachedFloor?.Invoke(CurrentFloor);
                }
            }
        }

        public void GoToFloor(int floor)
        {
            if (floor > AmountOfFloors)
            {
                throw new ArgumentOutOfRangeException(nameof(floor), "Requested floor is out of bounds!");
            }

            _targetFloor = floor;

            if (_targetFloor == CurrentFloor)
            {
                // already on the specified floor
                _stoppedUntil = Time.realtimeSinceStartup + _stopDuration;
                ReachedFloor?.Invoke(CurrentFloor);
                return;
            }

            // set direction based on target floor
            if (_currentFloor > _targetFloor)
            {
                CurrentDirection = Direction.Down;
            }
            else if (_currentFloor < _targetFloor)
            {
                CurrentDirection = Direction.Up;
            }
        }

        public void Park()
        {
            CurrentDirection = Direction.Stationary;
        }
    }
}