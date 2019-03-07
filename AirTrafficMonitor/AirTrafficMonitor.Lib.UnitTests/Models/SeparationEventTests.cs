using System;
using AirTrafficMonitor.Lib.Models;
using NUnit.Framework;

namespace AirTrafficMonitor.Lib.UnitTests.Models
{
    [TestFixture]
    public class SeparationEventTests
    {
        [TestCase("111111", "222222")]
        [TestCase("000000", "222222")] //to make sure we have not hardcoded some value
        public void Constructor_WhenCalledWithValidParams_SetsTag1Property(string tag1, string tag2)
        {
            var separationEvent = new SeparationEvent(tag1, tag2, DateTime.Now);

            Assert.That(separationEvent.Tag1, Is.EqualTo(tag1));
        }

        [TestCase("222222", "111111")]
        [TestCase("222222", "000000")] //to make sure we have not hardcoded some value
        public void Constructor_WhenCalledWithValidParams_SetsTag2Property(string tag1, string tag2)
        {
            var separationEvent = new SeparationEvent(tag1, tag2, DateTime.Now);

            Assert.That(separationEvent.Tag2, Is.EqualTo(tag2));
        }

        [TestCase("11111", "222222")]
        [TestCase("1111111", "222222")]
        [TestCase("111111", "22222")]
        [TestCase("111111", "2222222")]
        [TestCase("111111", "111111")] //invalid because the tags are equal
        [TestCase(null, "111111")] //invalid because the tags are equal
        [TestCase("111111", null)] //invalid because the tags are equal
        public void Constructor_WhenCalledWithInValidTagParam_ThrowsArgumentException(string tag1, string tag2)
        {
            Assert.That(() => new SeparationEvent(tag1, tag2, DateTime.Now), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Constructor_WhenCalledWithValidParams_SetsTimeStampObjectCase1()
        {
            var now = DateTime.Now;

            var separationEvent = new SeparationEvent("123456", "654321", now);

            Assert.That(separationEvent.Timestamp, Is.EqualTo(now));
        }

        [Test] //to make sure we have not hardcoded some value
        public void Constructor_WhenCalledWithValidParams_SetsTimeStampObjectCase2()
        {
            var yesterday = DateTime.Now.AddDays(-1);

            var separationEvent = new SeparationEvent("123456", "654321", yesterday);

            Assert.That(separationEvent.Timestamp, Is.EqualTo(yesterday));
        }
    }
}
