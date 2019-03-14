using System.Collections.Generic;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.EventArgs
{
    public class TrackEventArgs : System.EventArgs
    {
        public HashSet<ITrack> Trackings { get; }

        public TrackEventArgs(HashSet<ITrack> trackings)
        {
            Trackings = trackings;
        }
    }
}
