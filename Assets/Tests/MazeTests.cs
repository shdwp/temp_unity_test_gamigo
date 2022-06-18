using GraphLib;
using Maze;
using NUnit.Framework;

namespace Tests
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
            
            starting.SetUnidirectionalPath(GraphPathDirection.North, a);
            starting.SetUnidirectionalPath(GraphPathDirection.South, b);
            b.SetUnidirectionalPath(GraphPathDirection.South, c);
            c.SetUnidirectionalPath(GraphPathDirection.East, end);

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
            
            starting.SetUnidirectionalPath(GraphPathDirection.North, a);
            starting.SetUnidirectionalPath(GraphPathDirection.South, b);
            b.SetUnidirectionalPath(GraphPathDirection.South, c);
            c.SetUnidirectionalPath(GraphPathDirection.East, end);

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
            
            starting.SetUnidirectionalPath(GraphPathDirection.North, a);
            b.SetUnidirectionalPath(GraphPathDirection.South, c);
            c.SetUnidirectionalPath(GraphPathDirection.East, end);

            Assert.IsFalse(starting.PathExistsTo("end"));
        }

        [Test]
        public void TestNoRoom()
        {
            var starting = new Room("start");
            var a = new Room("a");
            var b = new Room("b");
            var c = new Room("c");
            
            starting.SetUnidirectionalPath(GraphPathDirection.North, a);
            b.SetUnidirectionalPath(GraphPathDirection.South, c);

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
            
            starting.SetUnidirectionalPath(GraphPathDirection.North, a);
            a.SetUnidirectionalPath(GraphPathDirection.South, a);
            
            starting.SetUnidirectionalPath(GraphPathDirection.South, b);
            b.SetUnidirectionalPath(GraphPathDirection.South, c);
            b.SetUnidirectionalPath(GraphPathDirection.West, b);
            c.SetUnidirectionalPath(GraphPathDirection.East, end);

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
            
            starting.SetBidirectionalPath(GraphPathDirection.North, a);
            starting.SetBidirectionalPath(GraphPathDirection.South, b);
            b.SetBidirectionalPath(GraphPathDirection.South, c);
            c.SetBidirectionalPath(GraphPathDirection.West, end);

            Assert.IsTrue(starting.PathExistsTo("end"));
        }
    }
}