using AirTrafficMonitor.Lib.Interfaces;
using AirTrafficMonitor.Lib.Models;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitor.Lib.UnitTests.Models
{
    [TestFixture]
    public class AirspaceTests
    {
        // These limits are defined in the assignment
        private const int LowerAltitude = 500;
        private const int UpperAltitude = 20000;
        private const int AirspaceSize = 80000;

        #region Constructor tests

        [TestCase(1,1)]
        [TestCase(-1,-1)]
        [TestCase(1,-1)]
        [TestCase(-1,1)]
        [TestCase(-100, 100)]
        public void Constructor_WhenCalled_SouthWestXIsAlwaysEqualToTheSuppliedCoordinate(int southWestX, int southWestY)
        {
            var coordinate = Substitute.For<ITwoDimensionalCoordinate>();
            coordinate.X.Returns(southWestX);
            coordinate.Y.Returns(southWestY);

            var airspace = new Airspace(coordinate);

            Assert.That(airspace.SouthWestCorner.X, Is.EqualTo(coordinate.X));
        }

        [TestCase(1, 1)]
        [TestCase(-1, -1)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        [TestCase(-100, 100)]
        public void Constructor_WhenCalled_SouthWestYIsAlwaysEqualToTheSuppliedCoordinate(int southWestX, int southWestY)
        {
            var coordinate = Substitute.For<ITwoDimensionalCoordinate>();
            coordinate.X.Returns(southWestX);
            coordinate.Y.Returns(southWestY);

            var airspace = new Airspace(coordinate);

            Assert.That(airspace.SouthWestCorner.Y, Is.EqualTo(coordinate.Y));
        }

        [TestCase(1, 1)]
        [TestCase(-1, -1)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        [TestCase(-100, 100)]
        public void Constructor_WhenCalled_LowerAltitudeBoundaryAlwaysEquals500(int southWestX, int southWestY)
        {
            var coordinate = Substitute.For<ITwoDimensionalCoordinate>();
            coordinate.X.Returns(southWestX);
            coordinate.Y.Returns(southWestY);

            var airspace = new Airspace(coordinate);

            Assert.That(airspace.LowerAltitudeBoundary, Is.EqualTo(LowerAltitude));
        }

        [TestCase(1, 1)]
        [TestCase(-1, -1)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        [TestCase(-100, 100)]
        public void Constructor_WhenCalled_UpperAltitudeBoundaryAlwaysEquals20000(int southWestX, int southWestY)
        {
            var coordinate = Substitute.For<ITwoDimensionalCoordinate>();
            coordinate.X.Returns(southWestX);
            coordinate.Y.Returns(southWestY);

            var airspace = new Airspace(coordinate);

            Assert.That(airspace.UpperAltitudeBoundary, Is.EqualTo(UpperAltitude));
        }

        [TestCase(1, 1)]
        [TestCase(-1, -1)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        [TestCase(-100, 100)]
        public void Constructor_WhenCalled_NorthEastXIsAlways80000BiggerThanTheSuppliedCoordinate(int southWestX, int southWestY)
        {
            var coordinate = Substitute.For<ITwoDimensionalCoordinate>();
            coordinate.X.Returns(southWestX);
            coordinate.Y.Returns(southWestY);

            var airspace = new Airspace(coordinate);

            Assert.That(airspace.NorthEastCorner.X, Is.EqualTo(coordinate.X + AirspaceSize));
        }

        [TestCase(1, 1)]
        [TestCase(-1, -1)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        [TestCase(-100, 100)]
        public void Constructor_WhenCalled_NorthEastYIsAlways80000BiggerThanTheSuppliedCoordinate(int southWestX, int southWestY)
        {
            var coordinate = Substitute.For<ITwoDimensionalCoordinate>();
            coordinate.X.Returns(southWestX);
            coordinate.Y.Returns(southWestY);

            var airspace = new Airspace(coordinate);

            Assert.That(airspace.NorthEastCorner.Y, Is.EqualTo(coordinate.Y + AirspaceSize));
        }

        #endregion
    }
}
