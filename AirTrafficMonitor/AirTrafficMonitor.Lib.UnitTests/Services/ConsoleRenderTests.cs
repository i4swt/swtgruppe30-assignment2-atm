using System;
using System.Collections.Generic;
using System.IO;
using AirTrafficMonitor.Lib.EventArgs;
using AirTrafficMonitor.Lib.Factories;
using AirTrafficMonitor.Lib.Interfaces;
using AirTrafficMonitor.Lib.Models;
using AirTrafficMonitor.Lib.Services;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;


namespace AirTrafficMonitor.Lib.UnitTests.Services
{
    [TestFixture]
    public class ConsoleRenderTests
    {
        private ConsoleRender uut;
        private IAirTrafficMonitorFactory fakeFactory;
        private ITransponderReceiver fakeTransponderReceiver;
        private AirTrafficMonitor fakeAirTrafficMonitor;
        private HashSet<ITrack> trackTestSet;
        private HashSet<ISeparationEvent> separationEventTestSet;


        [SetUp]
        public void SetUp()
        {
            fakeFactory = Substitute.For<IAirTrafficMonitorFactory>();
            fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            fakeAirTrafficMonitor = Substitute.For<AirTrafficMonitor>(fakeFactory,fakeTransponderReceiver);
            uut = new ConsoleRender(fakeAirTrafficMonitor);
            trackTestSet = new HashSet<ITrack>();
            separationEventTestSet = new HashSet<ISeparationEvent>();
        }

        #region RenderTrackings

        [Test]
        public void RenderTrackings_TrackingsEmpty_NothingIsWrittenToConsole()
        {
            
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                fakeAirTrafficMonitor.TrackingsChanged += Raise.EventWith(this, new TrackEventArgs(trackTestSet));

                string expected = string.Format("Current tracks in the airspace: {0}",Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());

            }
            
        }

        [Test]
        public void RenderTrackings_TrackingsContainData_DataIsWrittenToConsoleInCorrectFormat()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                trackTestSet.Add(new Track("ATR423;5645;14528;5000;20171006213456789"));
                trackTestSet.Add(new Track("ATR424;5343;65886;12500;20151006213456788"));
                trackTestSet.Add(new Track("ATR425;8568;457786;14000;20131006213456787"));

                fakeAirTrafficMonitor.TrackingsChanged += Raise.EventWith(this, new TrackEventArgs(trackTestSet));

                string expected = string.Format("Current tracks in the airspace: {0}", Environment.NewLine);
                expected += string.Format("Tag: ATR423, X: 5645, Y: 14528, Altitude: 5000 , Date: 10/06/2017 21:34:56 789, Velocity: Unknown, Heading: Unknown{0}",Environment.NewLine);
                expected += string.Format("Tag: ATR424, X: 5343, Y: 65886, Altitude: 12500 , Date: 10/06/2015 21:34:56 788, Velocity: Unknown, Heading: Unknown{0}", Environment.NewLine);
                expected += string.Format("Tag: ATR425, X: 8568, Y: 457786, Altitude: 14000 , Date: 10/06/2013 21:34:56 787, Velocity: Unknown, Heading: Unknown{0}", Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());

            }
        }

        #endregion

        #region RenderSeparationEvents

        [Test]
        public void RenderSeparationEvents_SeparationEventsIsEmpty_NothingIsWrittenToConsole()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                fakeAirTrafficMonitor.SeparationEventsChanged +=
                    Raise.EventWith(this, new SeparationEventArgs(separationEventTestSet));

                string expected = string.Format("Current separationevents in the airspace: {0}", Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void RenderSeparationEvents_SeparationEventsContainsData_DataIsWrittenToConsoleInCorrectFormat()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                separationEventTestSet.Add(new SeparationEvent("KLM123", "SAS123",
                    new DateTime(2005, 1, 1, 2, 30, 30, 500)));
                separationEventTestSet.Add(new SeparationEvent("JET999", "AIR454",
                    new DateTime(2005, 2, 2, 10, 10, 10, 550)));
                separationEventTestSet.Add(new SeparationEvent("IRE987", "SAS999",
                    new DateTime(2010, 10, 10, 10, 30, 30, 900)));

                fakeAirTrafficMonitor.SeparationEventsChanged +=
                    Raise.EventWith(this, new SeparationEventArgs(separationEventTestSet));

                string expected = string.Format("Current separationevents in the airspace: {0}", Environment.NewLine);

                expected += string.Format("Tag 1: KLM123, Tag 2: SAS123, Date: 01/01/2005 02:30:30 500{0}", Environment.NewLine);
                expected += string.Format("Tag 1: JET999, Tag 2: AIR454, Date: 02/02/2005 10:10:10 550{0}",
                    Environment.NewLine);
                expected += string.Format("Tag 1: IRE987, Tag 2: SAS999, Date: 10/10/2010 10:30:30 900{0}",
                    Environment.NewLine);

                Assert.AreEqual(expected, sw.ToString());
            }
        }

        #endregion
    }
}
