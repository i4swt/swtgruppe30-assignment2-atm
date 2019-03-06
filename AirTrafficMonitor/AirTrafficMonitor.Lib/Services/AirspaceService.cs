using System.Collections.Generic;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Services
{
    public class AirspaceService : IAirspaceService
    {
        public HashSet<ITrack> GetTrackingsInAirspace(HashSet<ITrack> trackings, IAirspace airspace)
        {
            var trackingsInAirspace = new HashSet<ITrack>();

            foreach (var track in trackings)
            {
                var isXInAirspace = track.Coordinate.X >= airspace.SouthWestCorner.X &&
                                    track.Coordinate.X <= airspace.NorthEastCorner.X;

                var isYInAirspace = track.Coordinate.Y >= airspace.SouthWestCorner.Y &&
                                    track.Coordinate.Y <= airspace.NorthEastCorner.Y;

                var isZInAirspace = track.Coordinate.Z >= airspace.LowerAltitudeBoundary &&
                                    track.Coordinate.Z <= airspace.UpperAltitudeBoundary;

                if (isXInAirspace && isYInAirspace && isZInAirspace)
                {
                    trackingsInAirspace.Add(track);
                }
            }

            return trackingsInAirspace;
        }
    }
}
