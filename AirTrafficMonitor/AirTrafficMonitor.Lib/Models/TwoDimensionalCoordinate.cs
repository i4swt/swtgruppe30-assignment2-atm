using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Models
{
    public class TwoDimensionalCoordinate : ITwoDimensionalCoordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public TwoDimensionalCoordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
