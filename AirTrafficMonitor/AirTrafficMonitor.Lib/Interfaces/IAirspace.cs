using System.Collections.Generic;

namespace AirTrafficMonitor.Lib.Interfaces
{
    public interface IAirspace
    {
        ICoordinate SouthWestCorner { get; set; }
        ICoordinate NorthEastCorner { get; set; }
        List<ITrack> GetTracksInAirspace(List<ITrack> tracks);
    }
}
