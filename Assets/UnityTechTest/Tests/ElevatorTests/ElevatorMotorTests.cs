using System;
using NUnit.Framework;
using UnityTechTest.Scripts.Elevator.Impl;
using UnityTechTest.Scripts.Elevator.Interfaces;
using UnityTechTest.Scripts.Elevator.Model;

namespace UnityTechTest.Tests.ElevatorTests
{
    public sealed class ElevatorMotorTests
    {
        [Test]
        public void TestGoToNextFloor()
        {
            IElevatorMotor motor = new ElevatorMotor(float.MaxValue, 0, 2);

            var reachedFloor = -1;
            motor.ReachedFloor += floor => reachedFloor = floor;
            motor.GoToFloor(1);

            foreach (var _ in ElevatorTestsUtils.RunUntilTestOrTimeout(() => !motor.StoppedOnAFloor))
            {
                motor.Update();
            }
            
            Assert.AreEqual(1, reachedFloor);
            Assert.AreEqual(1, motor.CurrentFloor);
            Assert.AreEqual(Direction.Up, motor.CurrentDirection);
        }
        
        [Test]
        public void TestGoToPreviousFloor()
        {
            IElevatorMotor motor = new ElevatorMotor(float.MaxValue, 0, 2);

            var reachedFloor = -1;
            motor.ReachedFloor += floor => reachedFloor = floor;
            motor.GoToFloor(1);
            foreach (var _ in ElevatorTestsUtils.RunUntilTestOrTimeout(() => !motor.StoppedOnAFloor))
            {
                motor.Update();
            }
            
            motor.GoToFloor(0);
            foreach (var _ in ElevatorTestsUtils.RunUntilTestOrTimeout(() => !motor.StoppedOnAFloor))
            {
                motor.Update();
            }
            
            Assert.AreEqual(0, reachedFloor);
            Assert.AreEqual(0, motor.CurrentFloor);
            Assert.AreEqual(Direction.Down, motor.CurrentDirection);
        }

        [Test]
        public void TestPark()
        {
            IElevatorMotor motor = new ElevatorMotor(float.MaxValue, 0, 2);
            
            motor.GoToFloor(1);
            foreach (var _ in ElevatorTestsUtils.RunUntilTestOrTimeout(() => !motor.StoppedOnAFloor))
            {
                motor.Update();
            }
            
            Assert.AreEqual(Direction.Up, motor.CurrentDirection);
            motor.Park();
            Assert.AreEqual(Direction.Stationary, motor.CurrentDirection);
        }

        [Test]
        public void TestMaxFloor()
        {
            IElevatorMotor motor = new ElevatorMotor(float.MaxValue, 0, 2);

            var reachedFloor = -1;
            motor.ReachedFloor += floor => reachedFloor = floor;
            motor.GoToFloor(2);

            foreach (var _ in ElevatorTestsUtils.RunUntilTestOrTimeout(() => !motor.StoppedOnAFloor))
            {
                motor.Update();
            }
            
            Assert.AreEqual(2, reachedFloor);
            Assert.AreEqual(2, motor.CurrentFloor);
            Assert.AreEqual(Direction.Up, motor.CurrentDirection);
        }

        [Test]
        public void TestFloorOutOfBounds()
        {
            IElevatorMotor motor = new ElevatorMotor(float.MaxValue, 0, 2);
            Assert.Catch<ArgumentOutOfRangeException>(() => motor.GoToFloor(3));
        }
    }
}