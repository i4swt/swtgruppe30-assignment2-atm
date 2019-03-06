using System.Collections.Generic;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.EventArgs
{
    public class SeparationEventArgs : System.EventArgs
    {
        public HashSet<ISeparationEvent> SeparationEvents { get; }

        public SeparationEventArgs(HashSet<ISeparationEvent> separationEvents)
        {
            SeparationEvents = separationEvents;
        }
    }
}
