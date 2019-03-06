using System;
using System.Globalization;
using NUnit.Framework;
using NUnit.Framework.Internal;
using AirTrafficMonitor.Lib.Models;

namespace AirTrafficMonitor.Lib.UnitTests.Models
{
    public class CoordinateTests
    {

        [Test]
        public void Ctor_CorrectlySetAttributes_XValid()
        {
            Coordinate coordinate = new Coordinate(1,2,3);
            Assert.That(coordinate.X, Is.EqualTo(1));
        }

        [Test]
        public void Ctor_CorrectlySetAttributes_YValid()
        {
            Coordinate coordinate = new Coordinate(1, 2, 3);
            Assert.That(coordinate.Y, Is.EqualTo(2));
        }

        [Test]
        public void Ctor_CorrectlySetAttributes_ZValid()
        {
            Coordinate coordinate = new Coordinate(1, 2, 3);
            Assert.That(coordinate.Z, Is.EqualTo(3));
        }
    }
}