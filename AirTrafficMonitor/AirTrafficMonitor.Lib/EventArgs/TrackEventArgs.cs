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
        public List<ITrack> Tracks { get; }

        public TrackEventArgs(List<ITrack> tracks)
        {
            Tracks = tracks;
        }
    }
}
