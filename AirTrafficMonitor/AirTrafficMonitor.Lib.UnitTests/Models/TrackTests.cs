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

        [TestCase("ATR423;39046;12932;14000;20151006213457789", ExpectedResult = 1)]
        [TestCase("ATR423;39045;12933;14000;20151006213457789", ExpectedResult = 1)]
        [TestCase("ATR423;39045;12934;14000;20151006213458789", ExpectedResult = 1)]
        [TestCase("ATR423;39046;12933;14000;20151006213457789", ExpectedResult = 1.4142135623730951)]
        [TestCase("ATR423;39044;12931;14000;20151006213457789", ExpectedResult = 1.4142135623730951)]
        public double Update_CheckCalculationOfVelocity_VelocityEqualToExpected(string rawDataUpdate)
        {
            var newTrackUpdate = new Track(rawDataUpdate);
            newTrackUpdate.Update(uut);
            Console.WriteLine(newTrackUpdate.Velocity);
            return newTrackUpdate.Velocity;
        }

        [TestCase("ATR423;39046;12932;14000;20151006213457789", ExpectedResult = 90)]//X
        [TestCase("ATR423;39044;12932;14000;20151006213457789", ExpectedResult = 270)]//-X
        [TestCase("ATR423;39045;12933;14000;20151006213457789", ExpectedResult = 0)]//Y
        [TestCase("ATR423;39045;12931;14000;20151006213457789", ExpectedResult = 180)]//-Y
        [TestCase("ATR423;39045;12934;14000;20151006213458789", ExpectedResult = 0)]//2Y
        [TestCase("ATR423;39046;12933;14000;20151006213457789", ExpectedResult = 225)]//X,Y
        [TestCase("ATR423;39044;12931;14000;20151006213457789", ExpectedResult = 135)]//-X,-Y


        public double Update_CheckCalculationOfHeading_HeadingEqualToExpected(string rawDataUpdate)
        {
            var newTrackUpdate = new Track(rawDataUpdate);
            newTrackUpdate.Update(uut);
            Console.WriteLine(newTrackUpdate.Heading);
            return newTrackUpdate.Heading;
        }


    }
}