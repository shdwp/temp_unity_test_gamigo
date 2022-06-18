using NUnit.Framework;
using Utils;
namespace Tests
{
    public class NumberUtilTests
    {
        [Test]
        public void TestOrdinary()
        {
            Assert.IsTrue(NumberUtils.AreDigitsUnique(123456789));
            Assert.IsFalse(NumberUtils.AreDigitsUnique(1234456789));
        }

        [Test]
        public void TestMinMax()
        {
            Assert.IsTrue(NumberUtils.AreDigitsUnique(uint.MinValue));
            Assert.IsFalse(NumberUtils.AreDigitsUnique(uint.MaxValue));
        }

        [Test]
        public void TestTrailingZeroes()
        {
            Assert.IsTrue(NumberUtils.AreDigitsUnique(1230));
            Assert.IsFalse(NumberUtils.AreDigitsUnique(10230));
        }
    }
}