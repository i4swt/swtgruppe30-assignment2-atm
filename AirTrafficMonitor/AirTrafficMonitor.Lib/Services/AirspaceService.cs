using System.Collections.Generic;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Services
{
    public class AirspaceService : IAirspaceService
    {
        /// <summary>
        /// Returns the trackings that are inside the specified airspace
        /// </summary>
        /// <param name="trackings">A collection of trackings</param>
        /// <param name="airspace">The airspace used to filter the trackings</param>
        /// <returns>The trackings that are inside the airspace</returns>
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
