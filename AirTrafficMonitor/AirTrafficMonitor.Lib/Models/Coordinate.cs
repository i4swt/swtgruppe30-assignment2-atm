using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Models
{
    public class Coordinate : ICoordinate
    {

        public Coordinate(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}