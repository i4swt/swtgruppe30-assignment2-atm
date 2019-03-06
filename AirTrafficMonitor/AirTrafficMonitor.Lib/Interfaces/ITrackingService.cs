using System;
using System.Collections.Generic;
using AirTrafficMonitor.Lib.EventArgs;

namespace AirTrafficMonitor.Lib.Interfaces
{
    public interface ITrackingService
    {
        HashSet<ITrack> CreateTrackings(List<string> rawData);
        HashSet<ITrack> UpdateTrackings(HashSet<ITrack> first, HashSet<ITrack> second);
    }
}