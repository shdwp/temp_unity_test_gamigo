using Ninject;
using UnityTechTest.Scripts.Elevator.Interfaces;
using UnityTechTest.Scripts.Elevator.Utils;

namespace UnityTechTest.Scripts.Elevator.Views
{
    public sealed class ElevatorBodyMediator
    {
        private readonly ElevatorBodyView _view;
        private readonly IElevatorMotor   _motor;

        public ElevatorBodyMediator(ElevatorBodyView view)
        {
            _view = view;
            _motor = Module.SharedKernel.Get<IElevatorMotor>();
        }

        public void OnEnable()
        {
            _motor.PositionUpdated += MotorPositionUpdated;
            _motor.ReachedFloor += OnReachedFloor;
        }
        
        public void OnDisable()
        {
            _motor.ReachedFloor -= OnReachedFloor;
            _motor.PositionUpdated -= MotorPositionUpdated;
        }
        
        private void MotorPositionUpdated(float pos)
        {
            _view.UpdatePosition(pos);
        }
        
        private void OnReachedFloor(int floor)
        {
            _view.ReachedFloor();
        }
    }
}