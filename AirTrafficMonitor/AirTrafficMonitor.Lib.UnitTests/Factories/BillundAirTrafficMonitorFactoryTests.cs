using AirTrafficMonitor.Lib.Factories;
using AirTrafficMonitor.Lib.Interfaces;
using NUnit.Framework;

namespace AirTrafficMonitor.Lib.UnitTests.Factories
{
    [TestFixture]
    public class BillundAirTrafficMonitorFactoryTests
    {
        private BillundAirTrafficMonitorFactory uut;

        [SetUp]
        public void SetUp()
        {
            uut = new BillundAirTrafficMonitorFactory();
        }

        [Test]
        public void SeparationServiceProperty_Get()
        {
            Assert.IsInstanceOf<ISeparationService>(uut.SeparationService);
        }

        [Test]
        public void TrackingServiceProperty_Get()
        {
            Assert.IsInstanceOf<ITrackingService>(uut.TrackingService);
        }

        [Test]
        public void AirspaceServiceProperty_Get()
        {
            Assert.IsInstanceOf<IAirspaceService>(uut.AirspaceService);
        }

        [Test]
        public void AirspaceProperty_Get()
        {
            Assert.IsInstanceOf<IAirspace>(uut.Airspace);
        }
    }
}