using System.Reflection;
using Ninject;
using Ninject.Modules;
using UnityTechTest.Scripts.Elevator.Impl;
using UnityTechTest.Scripts.Elevator.Interfaces;

namespace UnityTechTest.Scripts.Elevator.Utils
{
    public sealed class Module : NinjectModule
    {
        /// <summary>
        /// Singleton of ninject kernel implementing 
        /// </summary>
        public static KernelBase SharedKernel
        {
            get
            
            {
                if (_sharedKernel == null)
                {
                    _sharedKernel = new StandardKernel();
                    _sharedKernel.Load(Assembly.GetExecutingAssembly());
                }

                return _sharedKernel;
            }
        }

        private static KernelBase _sharedKernel;

        public override void Load()
        {
            Bind<IElevatorMotor>().ToConstant(new ElevatorMotor(1f, 1f, 11));
            Bind<IElevatorController>().To<ElevatorController>().InSingletonScope();
            Bind<IElevatorScheduler>().To<ElevatorScheduler>().InSingletonScope();
        }
    }
}