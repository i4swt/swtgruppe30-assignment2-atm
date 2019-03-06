using System.Collections.Generic;
using AirTrafficMonitor.Lib.Interfaces;
using AirTrafficMonitor.Lib.Services;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitor.Lib.UnitTests.Services
{
    [TestFixture]
    public class AirspaceServiceTests
    {
        private IAirspaceService _airspaceService;
        private IAirspace _airspace;

        #region Setup

        [SetUp]
        public void Setup()
        {
            _airspaceService = new AirspaceService();

            // An airspace consisting of a cube measuring 11x11x11 is constructed. The southwest corner is placed in (0,0)
            // The airspace is constructed in such a way that a x, y or z value of 5 is considered a "safe" value
            // because such a value is placed in the middle of the airspace
            var southWestCoordinate = Substitute.For<ITwoDimensionalCoordinate>();
            southWestCoordinate.X.Returns(0);
            southWestCoordinate.Y.Returns(0);

            var northEastCoordinate = Substitute.For<ITwoDimensionalCoordinate>();
            northEastCoordinate.X.Returns(10);
            northEastCoordinate.Y.Returns(10);

            _airspace = Substitute.For<IAirspace>();
            _airspace.LowerAltitudeBoundary.Returns(0);
            _airspace.UpperAltitudeBoundary.Returns(10);
            _airspace.SouthWestCorner.Returns(southWestCoordinate);
            _airspace.NorthEastCorner.Returns(northEastCoordinate);
        }

        #endregion

        #region GetTrackingsInAirspace Tests

        //Lower X boundary test
        [TestCase(1, 5, 5, 1)]
        [TestCase(0, 5, 5, 1)]
        [TestCase(-1, 5, 5, 0)]

        //Upper X boundary test
        [TestCase(9, 5, 5, 1)]
        [TestCase(10, 5, 5, 1)]
        [TestCase(11, 5, 5, 0)]

        //Lower Y boundary test
        [TestCase(5, 1, 5, 1)]
        [TestCase(5, 0, 5, 1)]
        [TestCase(5, -1, 5, 0)]

        //Upper Y boundary test
        [TestCase(5, 9, 5, 1)]
        [TestCase(5, 10, 5, 1)]
        [TestCase(5, 11, 5, 0)]

        //Lower altitude boundary test
        [TestCase(5, 5, 1, 1)]
        [TestCase(5, 5, 0, 1)]
        [TestCase(5, 5, -1, 0)]

        //Upper altitude boundary test
        [TestCase(5, 5, 9, 1)]
        [TestCase(5, 5, 10, 1)]
        [TestCase(5, 5, 11, 0)]
        public void GetTrackingsInAirspace_WhenCalled_ReturnsTrackingsInsideAirspaceOnly(int x, int y, int z, int expectedResult)
        {
            var trackings = new HashSet<ITrack>
                {
                    CreateTrack(x, y, z)
                };

            var trackingsInAirspace = _airspaceService.GetTrackingsInAirspace(trackings, _airspace);

            Assert.That(trackingsInAirspace.Count, Is.EqualTo(expectedResult));
        }


        //Added to test that the method will return all trackings in the airspace and no just a single one.
        [Test]
        public void GetTrackingsInAirspace_WhenCalled_ReturnsAllTrackingsInAirspace()
        {
            var trackings = new HashSet<ITrack>
                {
                    CreateTrack(4, 4, 4), // in airspace
                    CreateTrack(5, 5, 5), // in airspace
                    CreateTrack(-1, 5,5), // NOT in airspace
                    CreateTrack(6, 6, 6), // in airspace
                };

            var expectedResult = 3; //so we would expect 3 tracks in the airspace

            var trackingsInAirspace = _airspaceService.GetTrackingsInAirspace(trackings, _airspace);

            Assert.That(trackingsInAirspace.Count, Is.EqualTo(expectedResult));
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Helper method that creates a stubbed track with a coordinate based on the supplied values
        /// </summary>
        /// <param name="x">The x value of the tracks coordinate</param>
        /// <param name="y">The y value of the tracks coordinate</param>
        /// <param name="z">The z value of the tracks coordinate</param>
        /// <returns>A stubbed track that has a stubbed coordinate</returns>
        private ITrack CreateTrack(int x, int y, int z)
        {
            var coordinate = Substitute.For<ICoordinate>();
            coordinate.X.Returns(x);
            coordinate.Y.Returns(y);
            coordinate.Z.Returns(z);

            var track = Substitute.For<ITrack>();
            track.Coordinate.Returns(coordinate);

            return track;
        }

        #endregion
    }
}
