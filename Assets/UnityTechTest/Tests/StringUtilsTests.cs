using System;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnityTechTest.Scripts.Utils;
using Random = UnityEngine.Random;

namespace UnityTechTest.Tests
{
    public sealed class StringUtilsTests
    {
        [Test]
        public void TestOrdinary()
        {
            var input = Encoding.ASCII.GetBytes("abcdef");
            var order = Encoding.ASCII.GetBytes("fedcba");
            
            StringUtils.SortLetters(input, order);
            Assert.AreEqual("fedcba", Encoding.ASCII.GetString(input));
        }

        [Test]
        public void TestSpaces()
        {
            var input = Encoding.ASCII.GetBytes("def abc");
            var order = Encoding.ASCII.GetBytes(" abcdef");
            
            StringUtils.SortLetters(input, order);
            Assert.AreEqual(" abcdef", Encoding.ASCII.GetString(input));
        }

        [Test]
        public void TestRepeatingChars()
        {
            var input = Encoding.ASCII.GetBytes("fffabcced");
            var order = Encoding.ASCII.GetBytes("abcdef");
            
            StringUtils.SortLetters(input, order);
            Assert.AreEqual("abccdefff", Encoding.ASCII.GetString(input));
        }

        [Test]
        public void TestRandomLong()
        {
            var input = GetRandomString(1024);
            var order = Encoding.ASCII.GetBytes(" abcdef");
            var inputSorted = input.OrderBy(b => Array.IndexOf(order, b)).ToArray();
            
            StringUtils.SortLetters(input, order);
            Assert.AreEqual(Encoding.ASCII.GetString(inputSorted), Encoding.ASCII.GetString(input));
        }

        private byte[] GetRandomString(int length)
        {
            var result = new byte[length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = (byte)Random.Range('a', 'f' + 1);
            }

            return result;
        }
    }
}