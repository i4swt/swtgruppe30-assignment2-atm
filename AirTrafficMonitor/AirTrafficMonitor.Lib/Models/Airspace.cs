using System.Collections.Generic;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Models
{
    public class Airspace : IAirspace
    {
        public ICoordinate SouthWestCorner { get; set; }
        public ICoordinate NorthEastCorner { get; set; }
        public List<ITrack> GetTracksInAirspace(List<ITrack> tracks)
        {
            throw new System.NotImplementedException();
        }
    }
}