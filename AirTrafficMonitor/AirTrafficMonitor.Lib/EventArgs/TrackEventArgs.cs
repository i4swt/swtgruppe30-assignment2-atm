using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
