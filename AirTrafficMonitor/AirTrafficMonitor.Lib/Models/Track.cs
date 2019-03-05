using System;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Models
{
    public class Track : ITrack
    {
        public string Tag { get; set; }
        public int Velocity { get; set; }
        public int Heading { get; set; }
        public ICoordinate Coordinate { get; set; }
        public DateTime Timestamp { get; set; }
        public void Update(ITrack track)
        {
            throw new NotImplementedException();
        }
    }
}