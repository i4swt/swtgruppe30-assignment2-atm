using System;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Models
{
    public class SeparationEvent : ISeparationEvent
    {
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public DateTime Timestamp { get; set; }

        public SeparationEvent(string tag1, string tag2, DateTime timestamp)
        {
            Tag1 = tag1;
            Tag2 = tag2;
            Timestamp = timestamp;
        }
    }
}