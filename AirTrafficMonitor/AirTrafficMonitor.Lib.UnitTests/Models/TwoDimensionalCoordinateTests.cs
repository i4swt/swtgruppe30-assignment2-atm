using AirTrafficMonitor.Lib.Models;
using NUnit.Framework;

namespace AirTrafficMonitor.Lib.UnitTests.Models
{
    [TestFixture]
    public class TwoDimensionalCoordinateTests
    {
        [TestCase(1, 1, 1)]
        [TestCase(-10, 1, -10)]
        [TestCase(10, 1, 10)]
        public void Constructor_WhenCalled_SetsXProperty(int x, int y, int expectedResult)
        {
            var coordinate = new TwoDimensionalCoordinate(x, y);

            Assert.That(coordinate.X, Is.EqualTo(expectedResult));
        }

        [TestCase(1, 1, 1)]
        [TestCase(1, -10, -10)]
        [TestCase(1, 10, 10)]
        public void Constructor_WhenCalled_SetsYProperty(int x, int y, int expectedResult)
        {
            var coordinate = new TwoDimensionalCoordinate(x, y);

            Assert.That(coordinate.Y, Is.EqualTo(expectedResult));
        }
    }
}
