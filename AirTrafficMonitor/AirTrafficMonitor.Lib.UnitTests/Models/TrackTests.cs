using System;
using NUnit.Framework;
using NUnit.Framework.Internal;
using AirTrafficMonitor.Lib.Models;

namespace AirTrafficMonitor.Lib.UnitTests.Models
{
    [TestFixture]
    public class TrackTests
    {
        
        
        private Track uut;
        [SetUp]
        public void SetUp()
        {
            string RawData = "ATR423;39045;12932;14000;20151006213456789";
            uut = new Track(RawData);
        }


        [Test]
        public void Ctor_ParsedCorrectly_TagValid()
        {
           Assert.That(uut.Tag,Is.EqualTo("ATR423"));
        }

        [Test]
        public void Ctor_ParsedCorrectly_CoordinateXValid()
        {
            Assert.That(uut.Coordinate.X, Is.EqualTo(39045));
        }

        [Test]
        public void Ctor_ParsedCorrectly_CoordinateYValid()
        {
            Assert.That(uut.Coordinate.Y, Is.EqualTo(12932));
        }

        [Test]
        public void Ctor_ParsedCorrectly_CoordinateZValid()
        {
            Assert.That(uut.Coordinate.Z, Is.EqualTo(14000));
        }

        [Test]
        public void Ctor_ParsedCorrectly_TimestampValid()
        {
            Assert.That(uut.Timestamp.ToString() + " " + uut.Timestamp.Millisecond, Is.EqualTo("06-10-2015 21:34:56 789"));
        }

        [Test]
        public void Update_CheckCalculationOfVelocity_VelocityEqualTo1()
        {
            string RawDataUpdate = "ATR423;39046;12932;14000;20151006213457789";
            var newTrackUpdate = new Track(RawDataUpdate);
            newTrackUpdate.Update(uut);
            Assert.That(newTrackUpdate.Velocity,Is.EqualTo(1));
        }

    }
}