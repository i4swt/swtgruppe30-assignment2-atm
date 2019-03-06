using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Models
{
    public class TwoDimensionalCoordinate : ITwoDimensionalCoordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}