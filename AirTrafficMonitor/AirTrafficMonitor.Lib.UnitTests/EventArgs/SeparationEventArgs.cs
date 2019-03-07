using System;
using System.Collections.Generic;
using AirTrafficMonitor.Lib.EventArgs;
using AirTrafficMonitor.Lib.Interfaces;
using AirTrafficMonitor.Lib.Models;
using NUnit;
using NUnit.Framework;

namespace AirTrafficMonitor.Lib.UnitTests.EventArgs
{
    [TestFixture]
    public class SeparationEventArgsTest
    {
        public void Ctor_TestGetMethod_ContainsSameAsInserted()
        {
            HashSet<ISeparationEvent> toInsertIntoSeparationsEvent = new HashSet<ISeparationEvent>();
            SeparationEventArgs uut = new SeparationEventArgs(toInsertIntoSeparationsEvent);
            Assert.That(uut.SeparationEvents, Is.EqualTo(toInsertIntoSeparationsEvent));
        }

        public void Ctor_AddToList_BothListAreStillIdentical()
        {
            HashSet<ISeparationEvent> toInsertIntoSeparationsEvent = new HashSet<ISeparationEvent>();
            SeparationEventArgs uut = new SeparationEventArgs(toInsertIntoSeparationsEvent);

            uut.SeparationEvents.Add(new SeparationEvent("Tag1TT", "Tag2TT", DateTime.Now));

            Assert.That(uut.SeparationEvents, Is.EqualTo(toInsertIntoSeparationsEvent));
        }
    }
}
