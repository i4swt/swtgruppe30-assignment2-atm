using System.Collections.Generic;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Models
{
    public class Airspace : IAirspace
    {
        public ICoordinate SouthWestCorner { get; set; }
        public ICoordinate NorthEastCorner { get; set; }

        public Airspace(ICoordinate southWestCorner, ICoordinate northEastCorner)
        {
            SouthWestCorner = southWestCorner;
            NorthEastCorner = northEastCorner;
        }
    }
}