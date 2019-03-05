using System;

namespace AirTrafficMonitor.Lib.Interfaces
{
    public interface ISeparationEvent
    {
        string Tag1 { get; set; }
        string Tag2 { get; set; }
        DateTime Timestamp { get; set; }
    }
}