using System;
using System.Globalization;
using NUnit.Framework;
using NUnit.Framework.Internal;
using AirTrafficMonitor.Lib.Models;

namespace AirTrafficMonitor.Lib.UnitTests.Models
{
    public class ThreeDimensionalCoordinateTests
    {

        [Test]
        public void Ctor_CorrectlySetAttributes_XValid()
        {
            ThreeDimensionalCoordinate threeDimensionalCoordinate = new ThreeDimensionalCoordinate(1,2,3);
            Assert.That(threeDimensionalCoordinate.X, Is.EqualTo(1));
        }

        [Test]
        public void Ctor_CorrectlySetAttributes_YValid()
        {
            ThreeDimensionalCoordinate threeDimensionalCoordinate = new ThreeDimensionalCoordinate(1, 2, 3);
            Assert.That(threeDimensionalCoordinate.Y, Is.EqualTo(2));
        }

        [Test]
        public void Ctor_CorrectlySetAttributes_ZValid()
        {
            ThreeDimensionalCoordinate threeDimensionalCoordinate = new ThreeDimensionalCoordinate(1, 2, 3);
            Assert.That(threeDimensionalCoordinate.Z, Is.EqualTo(3));
        }
    }
}