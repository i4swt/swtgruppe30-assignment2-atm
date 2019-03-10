using System;
using System.Collections.Generic;
using AirTrafficMonitor.Lib.Interfaces;
using AirTrafficMonitor.Lib.Models;
using AirTrafficMonitor.Lib.Services;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitor.Lib.UnitTests.Services
{
    [TestFixture]
    public class TrackingServiceTests
    {
        private TrackingService uut;
        private List<string> _validDataListOfThreeTrackings;
        private HashSet<ITrack> _firstSetOfTracks;
        private HashSet<ITrack> _secondSetOfTracks;

        #region Setup
        [SetUp]
        public void SetUp()
        {
            uut = new TrackingService();

            _validDataListOfThreeTrackings = new List<string>()
            {
                "ATR423;5645;14528;5000;20171006213456789",
                "ATR424;5343;65886;12500;20151006213456788",
                "ATR425;8568;457786;14000;20131006213456787"
            };

            _firstSetOfTracks = new HashSet<ITrack>()
            {
                new Track("KLM123;35000;74000;10000;20190301203011456"),
                new Track("SAS999;35000;74000;10000;20190301203011456"),
                new Track("JET482;35000;74000;10000;20190301203011456"),
            };

            _secondSetOfTracks = new HashSet<ITrack>()
            {
                new Track("SAS999;33500;73000;9500;20190301202844776")
            };
        }
    #endregion

        #region CreateTrackings

        [Test]
        public void CreateTrackings_WithNullParameter_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => { uut.CreateTrackings(null); });
        }

        [Test]
        public void CreateTrackings_FromEmptyList_ReturnsEmptyHashSet()
        {
            var result = uut.CreateTrackings(new List<string>());
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void CreateTrackings_FromValidListOfTrackings_ReturnsHashSetOfEqualAmount()
        {
            var result = uut.CreateTrackings(_validDataListOfThreeTrackings);
            Assert.That(result.Count, Is.EqualTo(_validDataListOfThreeTrackings.Count));
        }

        [Test]
        [TestCase("ATR423;39045;12932;14000")] // Missing parameter
        [TestCase("ATR423;39045;12932;14000;20151006213456787;3456")] // Too many parameters
        [TestCase("ATR42;39045;12932;14000;20151006213456787")] // Tag too short
        [TestCase("ADGH125;39045;12932;14000;20151006213456787")] // Tag too long
        [TestCase("ATR423;f;12932;14000;20151006213456787")] // Invalid coordinate
        [TestCase("ATR423;547;kk;14000;20151006213456787")] // Invalid coordinate
        [TestCase("ATR423;547;12932;#;20151006213456787")] // Invalid coordinate
        [TestCase("ATR423;547;12932;14000;2015100621345678756")] // Date too long
        [TestCase("ATR423;547;12932;14000;2015100621345")] // Date too short
        [TestCase("ATR423;547;12932;14000;00001006213456787")] // Invalid year
        [TestCase("ATR423;547;12932;14000;20181306213456787")] // Invalid month
        [TestCase("ATR423;547;12932;14000;20181240213456787")] // Invalid date
        [TestCase("ATR423;547;12932;14000;20181240243456787")] // Invalid hour
        [TestCase("ATR423;547;12932;14000;20181240216156787")] // Invalid minute
        [TestCase("ATR423;547;12932;14000;20181240216156787")] // Invalid seconds

        public void CreateTrackings_FromInvalidListOfOne_ReturnsEmptyHashSet(string invalidTrackingData)
        {
            var result = uut.CreateTrackings(new List<string>() {invalidTrackingData});
            Assert.That(result.Count, Is.EqualTo(0));
        }

        #endregion

        #region UpdateTrackings
        [Test]
        public void UpdateTrackings_WithNullFirstArgument_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => { uut.UpdateTrackings(null, _secondSetOfTracks); });
        }

        [Test]
        public void UpdateTrackings_WithNullSecondArgument_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => { uut.UpdateTrackings(_firstSetOfTracks, null); });
        }

        [Test]
        public void UpdateTrackings_WithAllNullArguments_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => { uut.UpdateTrackings(null, null); });
        }

        [Test]
        public void UpdateTrackings_WithPopulatedArguments_ReturnsCorrectNumberOfTracks()
        {
            var result = uut.UpdateTrackings(_firstSetOfTracks, _secondSetOfTracks);
            Assert.That(result.Count, Is.EqualTo(_firstSetOfTracks.Count));
        }

        [Test]
        public void UpdateTrackings_WithPopulatedArguments_TrackRepresentedInBothSetsHasBeenUpdated()
        {
            var track1 = Substitute.For<ITrack>();
            var track2 = Substitute.For<ITrack>();
            var track3 = Substitute.For<ITrack>();
            var track4 = Substitute.For<ITrack>();

            var newTrackings = new HashSet<ITrack>()
            {
                track1, track2, track3, track4
            };

            var oldTrackings = new HashSet<ITrack>()
            {
                track4
            };

            uut.UpdateTrackings(newTrackings, oldTrackings);

            // Assert
            track4.Received(1).Update(Arg.Any<ITrack>());
        }

        [Test]
        public void UpdateTrackings_WithPopulatedArguments_TrackNotRepresentedInBothSetsHasNotBeenUpdated()
        {
            var track1 = Substitute.For<ITrack>();
            var track2 = Substitute.For<ITrack>();
            var track3 = Substitute.For<ITrack>();
            var track4 = Substitute.For<ITrack>();

            var newTrackings = new HashSet<ITrack>()
            {
                track1, track2, track3, track4
            };

            var oldTrackings = new HashSet<ITrack>()
            {
                track4
            };

            uut.UpdateTrackings(newTrackings, oldTrackings);

            // Assert
            track1.DidNotReceive().Update(Arg.Any<ITrack>());
        }

        #endregion
    }
}