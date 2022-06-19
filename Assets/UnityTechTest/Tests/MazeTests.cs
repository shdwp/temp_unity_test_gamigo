using NUnit.Framework;
using UnityTechTest.Scripts.Maze;

namespace UnityTechTest.Tests
{
    public sealed class MazeTests
    {
        [Test]
        public void TestOrdinaryPath()
        {
            var starting = new Room("start");
            var end = new Room("end");
            var a = new Room("a");
            var b = new Room("b");
            var c = new Room("c");
            
            starting.SetUnidirectionalPath(RoomPathDirection.North, a);
            starting.SetUnidirectionalPath(RoomPathDirection.South, b);
            b.SetUnidirectionalPath(RoomPathDirection.South, c);
            c.SetUnidirectionalPath(RoomPathDirection.East, end);

            Assert.IsTrue(starting.PathExistsTo("end"));
        }

        [Test]
        public void TestPathToSelf()
        {
            var starting = new Room("start");
            var end = new Room("end");
            var a = new Room("a");
            var b = new Room("b");
            var c = new Room("c");
            
            starting.SetUnidirectionalPath(RoomPathDirection.North, a);
            starting.SetUnidirectionalPath(RoomPathDirection.South, b);
            b.SetUnidirectionalPath(RoomPathDirection.South, c);
            c.SetUnidirectionalPath(RoomPathDirection.East, end);

            Assert.IsTrue(starting.PathExistsTo("start"));
        }

        [Test]
        public void TestNoPath()
        {
            var starting = new Room("start");
            var end = new Room("end");
            var a = new Room("a");
            var b = new Room("b");
            var c = new Room("c");
            
            starting.SetUnidirectionalPath(RoomPathDirection.North, a);
            b.SetUnidirectionalPath(RoomPathDirection.South, c);
            c.SetUnidirectionalPath(RoomPathDirection.East, end);

            Assert.IsFalse(starting.PathExistsTo("end"));
        }

        [Test]
        public void TestNoRoom()
        {
            var starting = new Room("start");
            var a = new Room("a");
            var b = new Room("b");
            var c = new Room("c");
            
            starting.SetUnidirectionalPath(RoomPathDirection.North, a);
            b.SetUnidirectionalPath(RoomPathDirection.South, c);

            Assert.IsFalse(starting.PathExistsTo("end"));
        }

        [Test]
        public void TestRecursive()
        {
            var starting = new Room("start");
            var end = new Room("end");
            var a = new Room("a");
            var b = new Room("b");
            var c = new Room("c");
            
            starting.SetUnidirectionalPath(RoomPathDirection.North, a);
            a.SetUnidirectionalPath(RoomPathDirection.South, a);
            
            starting.SetUnidirectionalPath(RoomPathDirection.South, b);
            b.SetUnidirectionalPath(RoomPathDirection.South, c);
            b.SetUnidirectionalPath(RoomPathDirection.West, b);
            c.SetUnidirectionalPath(RoomPathDirection.East, end);

            Assert.IsTrue(starting.PathExistsTo("end"));
        }

        [Test]
        public void TestBidirectional()
        {
            var starting = new Room("start");
            var end = new Room("end");
            var a = new Room("a");
            var b = new Room("b");
            var c = new Room("c");
            
            starting.SetBidirectionalPath(RoomPathDirection.North, a);
            starting.SetBidirectionalPath(RoomPathDirection.South, b);
            b.SetBidirectionalPath(RoomPathDirection.South, c);
            c.SetBidirectionalPath(RoomPathDirection.West, end);

            Assert.IsTrue(starting.PathExistsTo("end"));
        }
    }
}