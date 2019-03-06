using System;

namespace AirTrafficMonitor.Lib.Interfaces
{
    public interface ITrack
    {
        string Tag { get; set; }
        double Velocity { get; set; }
        int Heading { get; set; }
        ICoordinate Coordinate { get; set; }
        DateTime Timestamp { get; set; }
        void Update(ITrack track);
    }
}