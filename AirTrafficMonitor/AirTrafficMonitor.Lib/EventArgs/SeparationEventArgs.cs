using System.Collections.Generic;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.EventArgs
{
    public class SeparationEventArgs : System.EventArgs
    {
        public List<ISeparationEvent> SeparationEvents { get; }

        public SeparationEventArgs(List<ISeparationEvent> separationEvents)
        {
            SeparationEvents = separationEvents;
        }
    }
}
