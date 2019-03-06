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

            var compareValue = uut.SeparationEvents;

            Assert.That(compareValue, Is.EqualTo(toInsertIntoSeparationsEvent));
        }
    }
}
