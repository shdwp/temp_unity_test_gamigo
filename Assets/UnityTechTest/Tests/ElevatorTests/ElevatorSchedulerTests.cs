using System.Collections.Generic;
using NUnit.Framework;
using UnityTechTest.Scripts.Elevator.Impl;
using UnityTechTest.Scripts.Elevator.Interfaces;
using UnityTechTest.Scripts.Elevator.Model;

namespace UnityTechTest.Tests.ElevatorTests
{
    public sealed class ElevatorSchedulerTests
    {
        [Test]
        public void TestDestinationNextFloors()
        {
            IElevatorScheduler scheduler = new ElevatorScheduler();
            
            scheduler.AddRequest(new ElevatorRequest(ElevatorRequestType.Destination, 1));
            var serviced = new List<ElevatorRequest>();
            Assert.AreEqual(1, scheduler.NextTargetFloor(0, Direction.Up, true, serviced));
            Assert.AreEqual(0, serviced.Count);
            
            scheduler.AddRequest(new ElevatorRequest(ElevatorRequestType.Destination, 0));
            Assert.AreEqual(0, scheduler.NextTargetFloor(1, Direction.Down, true, serviced));
            Assert.AreEqual(1, serviced.Count);
        }

        [Test]
        public void TestDestinationDirectionChange()
        {
            IElevatorScheduler scheduler = new ElevatorScheduler();
            scheduler.AddRequest(new ElevatorRequest(ElevatorRequestType.Destination, 0));
            
            var serviced = new List<ElevatorRequest>();
            Assert.AreEqual(scheduler.NextTargetFloor(1, Direction.Up, true, serviced), 0);
            Assert.AreEqual(serviced.Count, 0);
            
            scheduler.AddRequest(new ElevatorRequest(ElevatorRequestType.Destination, 1));

            Assert.AreEqual(1, scheduler.NextTargetFloor(0, Direction.Down, true, serviced));
            Assert.AreEqual(1, serviced.Count);
        }

        [Test]
        public void TestDestinationNextSkippingOne()
        {
            IElevatorScheduler scheduler = new ElevatorScheduler();
            
            scheduler.AddRequest(new ElevatorRequest(ElevatorRequestType.Destination, 2));

            var serviced = new List<ElevatorRequest>();
            Assert.AreEqual(2, scheduler.NextTargetFloor(0, Direction.Up, true, serviced));
            Assert.AreEqual(0, serviced.Count);
        }

        [Test]
        public void TestDestinationCurrentFloor()
        {
            IElevatorScheduler scheduler = new ElevatorScheduler();
            
            scheduler.AddRequest(new ElevatorRequest(ElevatorRequestType.Destination, 0));
            var serviced = new List<ElevatorRequest>();
            var target = scheduler.NextTargetFloor(0, Direction.Up, true, serviced);
            
            Assert.AreEqual(null, target);
            Assert.AreEqual(1, serviced.Count);
        }

        [Test]
        public void TestSummonedSameDirectionNextFloor()
        {
            IElevatorScheduler scheduler = new ElevatorScheduler();
            
            var serviced = new List<ElevatorRequest>();
            scheduler.AddRequest(new ElevatorRequest(ElevatorRequestType.Summon, 1, Direction.Up));
            var target = scheduler.NextTargetFloor(0, Direction.Up, true, serviced);
            
            Assert.AreEqual(1, target);
            Assert.AreEqual(0, serviced.Count);
        }

        [Test]
        public void TestSummonedOppositeDirectionNextFloor()
        {
            IElevatorScheduler scheduler = new ElevatorScheduler();
            
            scheduler.AddRequest(new ElevatorRequest(ElevatorRequestType.Destination, 2));
            scheduler.AddRequest(new ElevatorRequest(ElevatorRequestType.Summon, 0, Direction.Down));
            var serviced = new List<ElevatorRequest>();
            Assert.AreEqual(2, scheduler.NextTargetFloor(0, Direction.Up, true, serviced));
        }

        [Test]
        public void TestTargetUpdateWhileMoving()
        {
            IElevatorScheduler scheduler = new ElevatorScheduler();
            var serviced = new List<ElevatorRequest>();
            
            scheduler.AddRequest(new ElevatorRequest(ElevatorRequestType.Destination, 5));
            Assert.AreEqual(5, scheduler.NextTargetFloor(0, Direction.Up, true, serviced));
            
            scheduler.AddRequest(new ElevatorRequest(ElevatorRequestType.Destination, 3));
            Assert.AreEqual(3, scheduler.NextTargetFloor(2, Direction.Up, false, serviced));
        }
        
        [Test]
        public void TestTargetNoUpdateWhileMoving()
        {
            IElevatorScheduler scheduler = new ElevatorScheduler();
            var serviced = new List<ElevatorRequest>();
            
            scheduler.AddRequest(new ElevatorRequest(ElevatorRequestType.Destination, 3));
            Assert.AreEqual(3, scheduler.NextTargetFloor(0, Direction.Up, true, serviced));
            
            scheduler.AddRequest(new ElevatorRequest(ElevatorRequestType.Destination, 5));
            Assert.AreEqual(3, scheduler.NextTargetFloor(2, Direction.Up, false, serviced));
        }
    }
}