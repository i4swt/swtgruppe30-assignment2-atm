using System.Runtime.InteropServices;
using AirTrafficMonitor.Lib.Interfaces;
using NUnit.Framework;
using NSubstitute;
//using TransponderReceiver;

namespace AirTrafficMonitor.Lib.UnitTests
{
    



    [TestFixture]
    public class AirTrafficMonitorTests
    {
        private AirTrafficMonitor uut;
        private IAirTrafficMonitorFactory mockFactory;
       // private ITransponderReceiver _fakeTransponderReceiver;
        [SetUp]
        public void Setup()
        {
            mockFactory = Substitute.For<IAirTrafficMonitorFactory>();
            mockFactory.Airspace = Substitute.For<IAirspace>();
            mockFactory.AirspaceService = Substitute.For<IAirspaceService>();
            mockFactory.SeparationService = Substitute.For<ISeparationService>();
            mockFactory.TrackingService = Substitute.For<ITrackingService>();

            uut = new AirTrafficMonitor(mockFactory);
        }

        [Test]
        public void TransportReceiver_DataReady_CallsFunctionInRightOrder_CorrectOrderCalled()
        {

        }

         
    }
}