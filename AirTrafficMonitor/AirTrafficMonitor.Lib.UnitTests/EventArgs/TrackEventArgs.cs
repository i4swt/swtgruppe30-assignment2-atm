using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Lib.EventArgs;
using AirTrafficMonitor.Lib.Interfaces;
using NUnit.Framework;

namespace AirTrafficMonitor.Lib.UnitTests.EventArgs
{
    [TestFixture]
    public class TrackEventArgsTest
    {
        public void Ctor_TestGetMethod_ContainsSameAsInserted()
        {
            HashSet<ITrack> toInsertIntoTrackEventArgs = new HashSet<ITrack>();
            TrackEventArgs uut = new TrackEventArgs(toInsertIntoTrackEventArgs);

            Assert.That(uut.Trackings, Is.EqualTo(toInsertIntoTrackEventArgs));
        }
    }
}
