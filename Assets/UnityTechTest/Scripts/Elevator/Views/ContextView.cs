using Ninject;
using UnityEngine;
using UnityTechTest.Scripts.Elevator.Interfaces;
using Module = UnityTechTest.Scripts.Elevator.Utils.Module;

namespace UnityTechTest.Scripts.Elevator.Views
{
    /// <summary>
    /// View that will control runtime of the services
    /// </summary>
    public sealed class ContextView : MonoBehaviour
    {
        private IElevatorController _controller;
        private IElevatorMotor      _motor;

        private void Awake()
        {
            _motor = Module.SharedKernel.Get<IElevatorMotor>();
            _controller = Module.SharedKernel.Get<IElevatorController>();
        }

        private void OnEnable()
        {
            _controller.Start();
        }

        private void OnDisable()
        {
            _controller.Stop();
        }

        private void Update()
        {
            _motor.Update();
        }
    }
}