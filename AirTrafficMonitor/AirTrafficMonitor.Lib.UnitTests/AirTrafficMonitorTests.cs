using System.Collections.Generic;
using System.Runtime.InteropServices;
using AirTrafficMonitor.Lib.Interfaces;
using AirTrafficMonitor.Lib.Models;
using NUnit.Framework;
using NSubstitute;
using TransponderReceiver;

namespace AirTrafficMonitor.Lib.UnitTests
{
    



    [TestFixture]
    public class AirTrafficMonitorTests
    {
        private AirTrafficMonitor uut;
        private IAirTrafficMonitorFactory mockFactory;
        private ITransponderReceiver _fakeTransponderReceiver;

        // private ITransponderReceiver _fakeTransponderReceiver;
        [SetUp]
        public void Setup()
        {
            Setup_Factory();

            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            uut = new AirTrafficMonitor(mockFactory, _fakeTransponderReceiver);
        }

        private void Setup_Factory()
        {
            mockFactory = Substitute.For<IAirTrafficMonitorFactory>();
            /*
            Commented these out. Factory has private setter now /Frank 

            mockFactory.Airspace = Substitute.For<IAirspace>();
            mockFactory.AirspaceService = Substitute.For<IAirspaceService>();
            mockFactory.SeparationService = Substitute.For<ISeparationService>();
            mockFactory.TrackingService = Substitute.For<ITrackingService>();*/
        }
        /* Might be included at some point, not relevant at current point. 
        [Test]
        public void TransportReceiver_DataReady_TrackingService_CreateTrackingCalled_FunctionCalledWithRightParams()
        {
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));


            mockFactory.TrackingService.Received().CreateTrackings(testData);
        }

        [Test]
        public void TransportReceiver_DataReady_airspaceService_GetTrackingsInAirspace_FunctionCalledWithRightParams()
        {
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");
            //This seems like a silly way to do it, having to build the flow.
            HashSet<ITrack> expectedReturns = new HashSet<ITrack>();
            foreach (var s in testData)
            {
                expectedReturns.Add(new Track(s));
            }

            mockFactory.TrackingService.CreateTrackings(Arg.Any<List<string>>())
                .Returns(expectedReturns);
           
            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            //Assert
            mockFactory.AirspaceService.Received().GetTrackingsInAirspace(expectedReturns, mockFactory.Airspace);
        }
        */
        [Test]
        public void TransportReceiver_DataReady_FunctionsCalledInOrder()
        {
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");

            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Received.InOrder(() =>
            {
                //Tracking
                mockFactory.TrackingService.CreateTrackings(Arg.Any<List<string>>());
                mockFactory.AirspaceService.GetTrackingsInAirspace(Arg.Any<HashSet<ITrack>>(), mockFactory.Airspace);
                mockFactory.TrackingService.UpdateTrackings(Arg.Any<HashSet<ITrack>>(), Arg.Any<HashSet<ITrack>>());
               
                //SeparationEvents
                mockFactory.SeparationService.GetAllSeparationEvents(Arg.Any<HashSet<ITrack>>());
                mockFactory.SeparationService.LogSeparationEvents(Arg.Any<HashSet<ISeparationEvent>>());
                mockFactory.SeparationService.UpdateSeparationEvents(Arg.Any<HashSet<ISeparationEvent>>(),
                    Arg.Any<HashSet<ISeparationEvent>>());

            });

        }

        [Test]
        public void TransportReceiver_DataReady_EventsGetsRaised_TrackingChangedEventHaveBeenRaised()
        {
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");

            //Subscribes to desired event
            bool TrackingsChangedCalled = false;
            uut.TrackingsChanged += (sender, args) => TrackingsChangedCalled = true;

            //Act
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));
           

            //Assert
            Assert.That(TrackingsChangedCalled,Is.EqualTo(true));
        }


        [Test]
        public void TransportReceiver_DataReady_EventsGetsRaised_SeparationsEventsChangedEventHaveBeenRaised()
        {
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");

            
            bool SeparationEventsChangedCalled = false;
            uut.SeparationEventsChanged += (sender, args) => SeparationEventsChangedCalled = true;

            //Act
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            //Assert
            Assert.That(SeparationEventsChangedCalled, Is.EqualTo(true));
        }

    }
}