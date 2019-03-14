using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Models
{
    public class ThreeDimensionalCoordinate : IThreeDimensionalCoordinate
    {

        public ThreeDimensionalCoordinate(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}