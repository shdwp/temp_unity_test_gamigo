using System;
using Ninject;
using UnityTechTest.Scripts.Elevator.Interfaces;
using UnityTechTest.Scripts.Elevator.Model;
using UnityTechTest.Scripts.Elevator.Utils;

namespace UnityTechTest.Scripts.Elevator.Views
{
    public sealed class ElevatorControlsMediator
    {
        private readonly ElevatorControlsView _view;
        private readonly IElevatorController  _controller;

        public ElevatorControlsMediator(ElevatorControlsView view)
        {
            _view = view;
            _controller = Module.SharedKernel.Get<IElevatorController>();
        }

        public void OnEnable()
        {
            _view.FloorButtonPushed += OnFloorButtonPushed;
            _view.SummonButtonPushed += OnSummonButtonPushed;
            
            _controller.ReachedDestinationFloor += OnReachedDestinationFloor;
            _controller.ReachedSummonedFloor += OnReachedSummonedFloor;
        }

        public void OnDisable()
        {
            _controller.ReachedSummonedFloor -= OnReachedSummonedFloor;
            _controller.ReachedDestinationFloor -= OnReachedDestinationFloor;
            
            _view.SummonButtonPushed -= OnSummonButtonPushed;
            _view.FloorButtonPushed -= OnFloorButtonPushed;
        }
        
        private void OnReachedDestinationFloor(int floor)
        {
            _view.ClearFloorButtonRequest(floor);
        }

        private void OnReachedSummonedFloor(int floor, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    _view.ClearSummonUpButtonRequest(floor);
                    break;
                
                case Direction.Down:
                    _view.ClearSummonDownButtonRequest(floor);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
        
        private void OnFloorButtonPushed(int floor)
        {
            _controller.FloorButtonPushed(floor);
        }
        
        private void OnSummonButtonPushed(int floor, Direction direction)
        {
            _controller.SummonButtonPushed(floor, direction);
        }
    }
}