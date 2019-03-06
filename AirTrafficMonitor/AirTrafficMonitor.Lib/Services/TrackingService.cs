using System;
using System.Collections.Generic;
using AirTrafficMonitor.Lib.EventArgs;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Services
{
    public class TrackingService : ITrackingService
    {
        public HashSet<ITrack> CreateTrackings(List<string> rawData)
        {
            throw new NotImplementedException();
        }

        public HashSet<ITrack> UpdateTrackings(HashSet<ITrack> first, HashSet<ITrack> second)
        {
            throw new NotImplementedException();
        }
    }
}