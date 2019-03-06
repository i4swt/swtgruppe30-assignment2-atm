using System;
using System.Collections.Generic;
using AirTrafficMonitor.Lib.EventArgs;

namespace AirTrafficMonitor.Lib.Interfaces
{
    public interface ISeparationService
    {
        
        void UpdateSeparationEvents(List<ITrack> tracks);
    }
}