using System.Globalization;
using NUnit.Framework;
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
            Assert.That(uut.Timestamp.ToString(CultureInfo.InvariantCulture) + " " + uut.Timestamp.Millisecond, Is.EqualTo("10/06/2015 21:34:56 789"));
        }

        [TestCase("ATR423;39046;12932;14000;20151006213457789", ExpectedResult = 1)]
        [TestCase("ATR423;39045;12933;14000;20151006213457789", ExpectedResult = 1)]
        [TestCase("ATR423;39045;12934;14000;20151006213458789", ExpectedResult = 1)]
        [TestCase("ATR423;39046;12933;14000;20151006213457789", ExpectedResult = 1.4142135623730951)]
        [TestCase("ATR423;39044;12931;14000;20151006213457789", ExpectedResult = 1.4142135623730951)]
        public double? Update_CheckCalculationOfVelocity_VelocityEqualToExpected(string rawDataUpdate)
        {
            var newTrackUpdate = new Track(rawDataUpdate);
            newTrackUpdate.Update(uut);
            
            return newTrackUpdate.Velocity;
        }

        [TestCase("ATR423;39046;12932;14000;20151006213457789", ExpectedResult = 90)]//X
        [TestCase("ATR423;39044;12932;14000;20151006213457789", ExpectedResult = 270)]//-X
        [TestCase("ATR423;39045;12933;14000;20151006213457789", ExpectedResult = 0)]//Y
        [TestCase("ATR423;39045;12931;14000;20151006213457789", ExpectedResult = 180)]//-Y
        [TestCase("ATR423;39045;12934;14000;20151006213458789", ExpectedResult = 0)]//2Y
        [TestCase("ATR423;39046;12933;14000;20151006213457789", ExpectedResult = 45)]//X,Y
        [TestCase("ATR423;39044;12931;14000;20151006213457789", ExpectedResult = 225)]//-X,-Y
        [TestCase("ATR423;39046;12931;14000;20151006213457789", ExpectedResult = 135)]//X,-Y
        [TestCase("ATR423;39044;12933;14000;20151006213457789", ExpectedResult = 315)]//-X,Y
        [TestCase("ATR423;39046;12934;14000;20151006213457789", ExpectedResult = 26)]//X,2Y
        public double? Update_CheckCalculationOfHeading_HeadingEqualToExpected(string rawDataUpdate)
        {
            var newTrackUpdate = new Track(rawDataUpdate);
            newTrackUpdate.Update(uut);
            return newTrackUpdate.Heading;
        }

        [TestCase("ATR423;39046;12932;14000;20151006213457789", ExpectedResult = true)]
        [TestCase("NEW423;39046;12932;14000;20151006213457789", ExpectedResult = false)]
        public bool GetHashCode_ValidateHashCodeBehaviour_ReturnsExpectedResultsFromComparison(string rawData)
        {
            var hashCode = uut.GetHashCode();
            Track newTrack = new Track(rawData);
            var SameTagHashCode = newTrack.GetHashCode();
            return hashCode == SameTagHashCode;
        }

        [Test]
        public void ToString_StringFormatAsExpected_TheOutputIsAsExpected()
        {
            string uutObjectAsString = uut.ToString();
            
            Assert.That(uutObjectAsString, Is.EqualTo("Tag: ATR423, X: 39045, Y: 12932, Altitude: 14000 , Date: 10/06/2015 21:34:56 789, Velocity: Unknown, Heading: Unknown"));
        }

        [Test]
        public void ToString_StringFormatAsExpecteOnUpdatedString_TheOutputIsAsExpected()
        {
            Track newTrackToUpdate = new Track("ATR423;39046;12932;14000;20151006213457789");

            newTrackToUpdate.Update(uut);
            string uutObjectAsString = newTrackToUpdate.ToString();
            
            Assert.That(uutObjectAsString, Is.EqualTo("Tag: ATR423, X: 39046, Y: 12932, Altitude: 14000 , Date: 10/06/2015 21:34:57 789, Velocity: 1.00, Heading: 90"));
        }



        [TestCase("ATR423;39046;12932;14000;20151006213457789", ExpectedResult = true)]
        [TestCase("NEW423;39046;12932;14000;20151006213457789", ExpectedResult = false)]
        [TestCase("ATR423;39043;12935;14030;20151006213457789", ExpectedResult = true)]
        [TestCase("NEW425;39043;12935;14030;20151006213457789", ExpectedResult = false)]
        [TestCase("ATT423;39043;12935;14030;20151006213457789", ExpectedResult = false)]
        public bool Equals_ValidateThatItCorrectOnlyEqualsOnTag_TagMatchIsTrue(string rawData)
        {
            Track newTrack = new Track(rawData);
            return uut.Equals(newTrack);
        }

    }
}