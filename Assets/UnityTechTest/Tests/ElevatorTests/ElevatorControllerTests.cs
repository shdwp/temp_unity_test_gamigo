using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityTechTest.Scripts.Elevator.Impl;
using UnityTechTest.Scripts.Elevator.Interfaces;
using UnityTechTest.Scripts.Elevator.Model;

namespace UnityTechTest.Tests.ElevatorTests
{
    public sealed class ElevatorControllerTests
    {
        [Test]
        public void TestMotorGoToFloor()
        {
            var motor = Substitute.For<IElevatorMotor>();
            var scheduler = Substitute.For<IElevatorScheduler>();
            var controller = new ElevatorController(motor, scheduler);

            motor.CurrentFloor.Returns(0);
            motor.CurrentDirection.Returns(Direction.Stationary);
            motor.StoppedOnAFloor.Returns(true);
            scheduler.NextTargetFloor(motor.CurrentFloor, motor.CurrentDirection, motor.StoppedOnAFloor, Arg.Any<List<ElevatorRequest>>()).Returns(1);

            controller.FloorButtonPushed(1);

            scheduler.Received().AddRequest(new ElevatorRequest(ElevatorRequestType.Destination, 1));
            motor.Received().GoToFloor(1);
        }

        [Test]
        public void TestMotorPark()
        {
            var motor = Substitute.For<IElevatorMotor>();
            var scheduler = Substitute.For<IElevatorScheduler>();
            var controller = new ElevatorController(motor, scheduler);

            motor.CurrentFloor.Returns(0);
            motor.CurrentDirection.Returns(Direction.Stationary);
            motor.StoppedOnAFloor.Returns(true);
            scheduler.NextTargetFloor(motor.CurrentFloor, motor.CurrentDirection, motor.StoppedOnAFloor, Arg.Any<List<ElevatorRequest>>());

            controller.FloorButtonPushed(1);

            scheduler.Received().AddRequest(new ElevatorRequest(ElevatorRequestType.Destination, 1));
            motor.Received().Park();
        }

        [Test]
        public void TestFloorButtonPushed()
        {
            var motor = Substitute.For<IElevatorMotor>();
            var scheduler = Substitute.For<IElevatorScheduler>();
            var controller = new ElevatorController(motor, scheduler);

            controller.FloorButtonPushed(1);
            scheduler.Received().AddRequest(new ElevatorRequest(ElevatorRequestType.Destination, 1));
        }

        [Test]
        public void TestSummonButtonPushed()
        {
            var motor = Substitute.For<IElevatorMotor>();
            var scheduler = Substitute.For<IElevatorScheduler>();
            var controller = new ElevatorController(motor, scheduler);

            controller.SummonButtonPushed(1, Direction.Up);
            scheduler.Received().AddRequest(new ElevatorRequest(ElevatorRequestType.Summon, 1, Direction.Up));
        }

        [Test]
        public void TestDestinationNotification()
        {
            var motor = Substitute.For<IElevatorMotor>();
            var scheduler = Substitute.For<IElevatorScheduler>();
            var controller = new ElevatorController(motor, scheduler);

            var reachedDestination = -1;
            var reachedSummon = -1;
            controller.ReachedSummonedFloor += (floor, _) => reachedSummon = floor;
            controller.ReachedDestinationFloor += floor => reachedDestination = floor;

            motor.CurrentFloor.Returns(0);
            motor.CurrentDirection.Returns(Direction.Stationary);
            motor.StoppedOnAFloor.Returns(true);
            scheduler.NextTargetFloor(
                motor.CurrentFloor,
                motor.CurrentDirection,
                motor.StoppedOnAFloor,
                Arg.Do<List<ElevatorRequest>>(l => l.Add(new ElevatorRequest(ElevatorRequestType.Destination, 1)))
            );

            controller.FloorButtonPushed(1);
            Assert.AreEqual(1, reachedDestination);
            Assert.AreEqual(-1, reachedSummon);
        }

        [Test]
        public void TestSummonNotification()
        {
            var motor = Substitute.For<IElevatorMotor>();
            var scheduler = Substitute.For<IElevatorScheduler>();
            var controller = new ElevatorController(motor, scheduler);

            var reachedDestination = -1;
            var reachedSummon = -1;
            var reachedSummonDirection = Direction.Stationary;
            controller.ReachedSummonedFloor += (floor, dir) =>
            {
                reachedSummon = floor;
                reachedSummonDirection = dir;
            };
            controller.ReachedDestinationFloor += floor => reachedDestination = floor;

            motor.CurrentFloor.Returns(0);
            motor.CurrentDirection.Returns(Direction.Stationary);
            motor.StoppedOnAFloor.Returns(true);
            scheduler.NextTargetFloor(
                motor.CurrentFloor,
                motor.CurrentDirection,
                motor.StoppedOnAFloor,
                Arg.Do<List<ElevatorRequest>>(l => l.Add(new ElevatorRequest(ElevatorRequestType.Summon, 1, Direction.Up)))
            );

            controller.FloorButtonPushed(1);
            Assert.AreEqual(-1, reachedDestination);
            Assert.AreEqual(1, reachedSummon);
            Assert.AreEqual(Direction.Up, reachedSummonDirection);
        }
    }
}