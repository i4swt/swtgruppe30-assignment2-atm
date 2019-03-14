using System.Collections.Generic;

namespace AirTrafficMonitor.Lib.Interfaces
{
    public interface ITrackingService
    {
        HashSet<ITrack> CreateTrackings(List<string> rawData);
        HashSet<ITrack> UpdateTrackings(HashSet<ITrack> first, HashSet<ITrack> second);
    }
}