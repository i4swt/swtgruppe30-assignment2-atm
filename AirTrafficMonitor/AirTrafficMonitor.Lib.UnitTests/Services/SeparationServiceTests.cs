using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AirTrafficMonitor.Lib.Interfaces;
using AirTrafficMonitor.Lib.Models;
using AirTrafficMonitor.Lib.Services;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace AirTrafficMonitor.Lib.UnitTests.Services
{
    [TestFixture]
    public class SeparationServiceTests
    {
        private SeparationService uut;
        private ILoggingService fakeLoggingService;
        private HashSet<ISeparationEvent> _firstSetOfSeparationEvents;
        private HashSet<ISeparationEvent> _secondSetOfSeparationEvents;
        private HashSet<ITrack> _setOfTracks;

        #region Setup
        [SetUp]
        public void Setup()
        {
            fakeLoggingService = Substitute.For<ILoggingService>();
            uut = new SeparationService(fakeLoggingService);

        }

        #endregion

        //DONE
        #region GetAllSeparationEvents

        [Test]
        public void GetAllSeparationEvents_TrackSetEqualsNull_ExceptionThrown()
        {
            Assert.That(() => uut.GetAllSeparationEvents(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void GetAllSeparationEvents_EmptyTrackSet_ReturnEmptySeparationEventSet()
        {
            _setOfTracks = new HashSet<ITrack>();

            Assert.That(uut.GetAllSeparationEvents(_setOfTracks).Count==0);
        }

        [Test]
        public void GetAllSeparationEvents_TrackSetWithoutEvents_ReturnEmptySeparationEventSet()
        {
            _setOfTracks = new HashSet<ITrack>();
            ITrack i1 = new Track("KLM123;35000;74000;10000;20190301203011456");
            ITrack i2 = new Track("SAS999;35000;74000;10000;20190301203011456");
            i1.Coordinate.X = 0;
            i1.Coordinate.Y = 0;
            i1.Coordinate.Z = 0;
            i2.Coordinate.X = 0;
            i2.Coordinate.Y = 5000;
            i2.Coordinate.Z = 300;
            _setOfTracks.Add(i1);
            _setOfTracks.Add(i2);

            Assert.That(uut.GetAllSeparationEvents(_setOfTracks).Count==0);
        }

        [Test]
        public void GetAllSeparationEvents_TrackSetWithOneEvent_ReturnSetWithOneEvent()
        {
            _setOfTracks = new HashSet<ITrack>();
            ITrack i1 = new Track("KLM123;35000;74000;10000;20190301203011456");
            ITrack i2 = new Track("SAS999;35000;74000;10000;20190301203011456");
            i1.Coordinate.X = 0;
            i1.Coordinate.Y = 0;
            i1.Coordinate.Z = 0;
            i2.Coordinate.X = 0;
            i2.Coordinate.Y = 4999;
            i2.Coordinate.Z = 299;
            _setOfTracks.Add(i1);
            _setOfTracks.Add(i2);

            Assert.That(uut.GetAllSeparationEvents(_setOfTracks).Count == 1);
        }

        [Test]
        public void GetAllSeparationEvents_TrackSetWithThreeEvents_ReturnSetWithThreeEvents()
        {
            _setOfTracks = new HashSet<ITrack>();
            ITrack i1 = new Track("KLM123;35000;74000;10000;20190301203011456");
            ITrack i2 = new Track("SAS999;35000;74000;10000;20190301203011456");
            ITrack i3 = new Track("SAS123;35000;74000;10000;20190301203011456");
            i1.Coordinate.X = 0;
            i1.Coordinate.Y = 0;
            i1.Coordinate.Z = 0;
            i2.Coordinate.X = 0;
            i2.Coordinate.Y = 0;
            i2.Coordinate.Z = 0;
            i3.Coordinate.X = 0;
            i3.Coordinate.Y = 0;
            i3.Coordinate.Z = 0;
            _setOfTracks.Add(i1);
            _setOfTracks.Add(i2);
            _setOfTracks.Add(i3);

            Assert.That(uut.GetAllSeparationEvents(_setOfTracks).Count == 3);
        }

        #endregion

        //DONE
        #region RaiseSeparationEvent

        [Test]
        [TestCase(0,0,1000,0,5000,1300,false)]
        [TestCase(0,0,1000,0,4999,1299,true)]
        [TestCase(0,0,-500,0,0,-300,true)]
        [TestCase(-1500,-1500,0,1500,1500,0,true)]
        [TestCase(-2000,-2000,0,2000,2000,0,false)]
        [TestCase(-2000,2000,0,2000,2000,0,true)]
        [TestCase(-2000, 2000, 0, 2000, 5000, 0, false)]
        [TestCase(2000, 2000, 0, 2000, -2000, 0, true)]
        [TestCase(2000, 2000, 0, 2000, -3000, 0, false)]
        public void RaiseSeparationEvents(int x1, int y1, int z1, int x2, int y2, int z2, bool separationEventOccured)
        {
            ITrack i1 = new Track("KLM123;35000;74000;10000;20190301203011456");
            ITrack i2 = new Track("SAS999;35000;74000;10000;20190301203011456");

            i1.Coordinate.X = x1;
            i1.Coordinate.Y = y1;
            i1.Coordinate.Z = z1;

            i2.Coordinate.X = x2;
            i2.Coordinate.Y = y2;
            i2.Coordinate.Z = z2;

            Assert.That(uut.RaiseSeparationEvent(i1,i2)==separationEventOccured);
        }

        #endregion

        //DONE
        #region GetNewSeparationEvents

        [Test]
        public void GetNewSeparationEvents_allSeparationEventsEqualsNull_ExceptionThrown()
        {
            Assert.That(() => uut.GetNewSeparationEvents(null, _firstSetOfSeparationEvents), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void GetNewSeparationEvents_oldSeparationEventsEqualsNull_ExceptionThrown()
        {
            Assert.That(()=>uut.GetNewSeparationEvents(_firstSetOfSeparationEvents, null), Throws.TypeOf<ArgumentNullException>() );
        }

        [Test]
        public void GetNewSeparationEvents_NewListEmpty_ReturnsEmptyList()
        {
            _firstSetOfSeparationEvents=new HashSet<ISeparationEvent>();
            _secondSetOfSeparationEvents = new HashSet<ISeparationEvent>();

            Assert.That(uut.GetNewSeparationEvents(_firstSetOfSeparationEvents,_secondSetOfSeparationEvents).Count==0);
        }

        [Test]
        public void GetNewSeparationEvents_NoNewSeparationEvents_ReturnsEmptyList()
        {
            _firstSetOfSeparationEvents = new HashSet<ISeparationEvent>()
            {
                new SeparationEvent("KLM123", "SAS123",new DateTime(2000,1,1,5,0,0)),
                new SeparationEvent("SAS999","KLM132",new DateTime(2000,1,1,5,0,0)),
            };

            _secondSetOfSeparationEvents = new HashSet<ISeparationEvent>()
            {
                new SeparationEvent("KLM123", "SAS123",new DateTime(2000,1,1,5,0,0)),
                new SeparationEvent("SAS999","KLM132",new DateTime(2000,1,1,5,0,0)),
            };

            Assert.That(uut.GetNewSeparationEvents(_firstSetOfSeparationEvents,_secondSetOfSeparationEvents).Count==0);
        }

        [Test]
        public void GetNewSeparationEvents_OneNewEventAndNoOldEvents_ReturnListOfOneElement()
        {
            _firstSetOfSeparationEvents = new HashSet<ISeparationEvent>()
            {
                new SeparationEvent("KLM123", "SAS123",new DateTime(2000,1,1,5,0,0)),
            };

            _secondSetOfSeparationEvents = new HashSet<ISeparationEvent>();

            Assert.That(uut.GetNewSeparationEvents(_firstSetOfSeparationEvents,_secondSetOfSeparationEvents).Count==1);
        }

        [Test]
        public void GetNewSeparationEvents_OneNewEventWithOldEvent_ReturnListOfOneElement()
        {
            _firstSetOfSeparationEvents = new HashSet<ISeparationEvent>()
            {
                new SeparationEvent("KLM123", "SAS123",new DateTime(2000,1,1,5,0,0)),
            };

            _secondSetOfSeparationEvents = new HashSet<ISeparationEvent>()
            {
                new SeparationEvent("KLM999", "SAS123", new DateTime(2000, 1, 1, 5, 0, 0)),
            };

            Assert.That(uut.GetNewSeparationEvents(_firstSetOfSeparationEvents,_secondSetOfSeparationEvents).Count==1);
        }

        [Test]
        public void GetNewSeparationEvents_OneOldEventWithNewTimestamp_EmptyListReturned()
        {
            _firstSetOfSeparationEvents = new HashSet<ISeparationEvent>()
            {
                new SeparationEvent("KLM123", "SAS123",new DateTime(2000,1,1,5,0,0)),
            };

            _secondSetOfSeparationEvents = new HashSet<ISeparationEvent>()
            {
                new SeparationEvent("KLM123", "SAS123", new DateTime(2007, 1, 1, 5, 0, 0)),
            };

            Assert.That(uut.GetNewSeparationEvents(_firstSetOfSeparationEvents, _secondSetOfSeparationEvents).Count == 0);
        }

        #endregion

        //DONE
        #region LogSeparationEvents

        [Test]
        [TestCase("12/24/2005 06:30:00 , KLM123 , SAS999")]
        [TestCase("01/20/2006 05:00:00 , KLM999 , JET253")]
        public void LogSeparationEvents_CallFunction_AllSeparationEventsLogged(string output)
        {
            _firstSetOfSeparationEvents = new HashSet<ISeparationEvent>()
            {
                new SeparationEvent("KLM123","SAS999",new DateTime(2005,12,24,6,30,0)),
                new SeparationEvent("KLM999","JET253",new DateTime(2006,1,20,5,0,0))
            };

            uut.LogSeparationEvents(_firstSetOfSeparationEvents);

            //assert that
            fakeLoggingService.Received(1).Log(output);
        }
        #endregion

        //DONE
        #region UpdateSeparationEvents

        [Test]
        public void UpdateSeparationEvents_FirstListEqualsNull_ExceptionThrown()
        {
            Assert.That(() => uut.UpdateSeparationEvents(null, _firstSetOfSeparationEvents), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void UpdateSeparationEvents_SecondListEqualsNull_ExceptionThrown()
        {
            Assert.That(() => uut.UpdateSeparationEvents(_firstSetOfSeparationEvents, null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void UpdateSeparationEvents_AllSeparationEventsEmpty_ReturnEmptyList()
        {
            _firstSetOfSeparationEvents=new HashSet<ISeparationEvent>();
            _secondSetOfSeparationEvents = new HashSet<ISeparationEvent>()
            {
                new SeparationEvent("KLM123", "SAS123", new DateTime(2005, 1, 1, 5, 0, 0)),
            };

            Assert.That(uut.UpdateSeparationEvents(_firstSetOfSeparationEvents,_secondSetOfSeparationEvents).Count==0);
        }

        [Test]
        public void UpdateSeparationEvents_OldSeparationEventsEmpty_ReturnsOneEvent()
        {
            _firstSetOfSeparationEvents = new HashSet<ISeparationEvent>();
            _secondSetOfSeparationEvents = new HashSet<ISeparationEvent>()
            {
                new SeparationEvent("KLM123", "SAS123", new DateTime(2005, 1, 1, 5, 0, 0)),
            };

            Assert.That(uut.UpdateSeparationEvents(_secondSetOfSeparationEvents, _firstSetOfSeparationEvents).Count==1);
        }

        [Test]
        public void UpdateSeparationEvents_EventsWithIdenticalTags_OldEventIsKept()
        {
            _firstSetOfSeparationEvents = new HashSet<ISeparationEvent>()
            {
                new SeparationEvent("KLM123", "SAS123", new DateTime(2008, 1, 1, 5, 0, 0)),
            };

            _secondSetOfSeparationEvents = new HashSet<ISeparationEvent>()
            {
                new SeparationEvent("KLM123", "SAS123", new DateTime(2005, 1, 1, 5, 0, 0)),
            };

            HashSet<ISeparationEvent> testSet =
                uut.UpdateSeparationEvents(_firstSetOfSeparationEvents, _secondSetOfSeparationEvents);

            Assert.That(testSet.First().Timestamp == new DateTime(2005, 1, 1, 5, 0, 0));
        }

        [Test]
        public void UpdateSeparationEvents_EventNoLongerActive_ReturnsListWithoutOldEvents()
        {
            _firstSetOfSeparationEvents = new HashSet<ISeparationEvent>()
            {
                new SeparationEvent("KLM123", "SAS999", new DateTime(2008, 1, 1, 5, 0, 0)),
            };

            _secondSetOfSeparationEvents = new HashSet<ISeparationEvent>()
            {
                new SeparationEvent("KLM123", "SAS123", new DateTime(2005, 1, 1, 5, 0, 0)),
                new SeparationEvent("JET123","SAS123",new DateTime(2000,1,1,2,0,0)),
            };

            HashSet<ISeparationEvent> testSet = uut.UpdateSeparationEvents(_firstSetOfSeparationEvents, _secondSetOfSeparationEvents);
            Assert.That(testSet.Count == 1 && testSet.First().Tag2 == "SAS999");
        }

        #endregion
    }
}