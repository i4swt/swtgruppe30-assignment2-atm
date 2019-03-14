using System.Collections.Generic;
using AirTrafficMonitor.Lib.EventArgs;
using AirTrafficMonitor.Lib.Interfaces;
using AirTrafficMonitor.Lib.Models;
using NUnit.Framework;

namespace AirTrafficMonitor.Lib.UnitTests.EventArgs
{
    [TestFixture]
    public class TrackEventArgsTests
    {
        [Test]
        public void Ctor_TestGetMethod_ContainsSameAsInserted()
        {
            HashSet<ITrack> toInsertIntoTrackEventArgs = new HashSet<ITrack>();
            TrackEventArgs uut = new TrackEventArgs(toInsertIntoTrackEventArgs);

            Assert.That(uut.Trackings, Is.EqualTo(toInsertIntoTrackEventArgs));
        }

        [Test]
        public void Ctor_AddToList_BothListStillIdentical()
        {
            HashSet<ITrack> toInsertIntoTrackEventArgs = new HashSet<ITrack>();
            TrackEventArgs uut = new TrackEventArgs(toInsertIntoTrackEventArgs);

            string ValidTrackString = "NEW423;39046;12932;14000;20151006213457789";
            uut.Trackings.Add(new Track(ValidTrackString));
            Assert.That(uut.Trackings, Is.EqualTo(toInsertIntoTrackEventArgs));
        }
    }
}
