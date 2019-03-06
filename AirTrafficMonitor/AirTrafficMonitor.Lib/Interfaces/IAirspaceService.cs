using System.Collections.Generic;

namespace AirTrafficMonitor.Lib.Interfaces
{
    public interface IAirspaceService
    {
        HashSet<ITrack> GetTrackingsInAirspace(HashSet<ITrack> trackings, IAirspace airspace);
    }
}