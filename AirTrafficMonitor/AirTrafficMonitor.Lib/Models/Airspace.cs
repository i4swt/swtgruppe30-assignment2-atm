using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Models
{
    public class Airspace : IAirspace
    {
        public ITwoDimensionalCoordinate SouthWestCorner { get; }
        public ITwoDimensionalCoordinate NorthEastCorner { get; }

        public int LowerAltitudeBoundary { get; }
        public int UpperAltitudeBoundary { get; }

        /// <summary>
        /// Constructs an airspace. The location of the airspace is defined by its south west corner.
        /// The airspace will always measure a square of 80000x80000 meters within altitudes of 500-20000 meters.
        /// </summary>
        /// <param name="southWestCorner">The southwest corner of the airspace</param>
        public Airspace(ITwoDimensionalCoordinate southWestCorner)
        {
            // These constants are defined in the assignment
            const int lowerAltitude = 500;
            const int upperAltitude = 20000;
            const int airspaceSize = 80000;

            SouthWestCorner = southWestCorner;

            // Define the northeast corner based on the southwest corner and the above constants
            NorthEastCorner = new TwoDimensionalCoordinate(southWestCorner.X + airspaceSize, southWestCorner.Y + airspaceSize);

            //The lower and upper altitudes are defined in the assignment
            LowerAltitudeBoundary = lowerAltitude; 
            UpperAltitudeBoundary = upperAltitude;
        }
    }
}
