using System;
using System.Collections.Generic;
using AirTrafficMonitor.Lib.EventArgs;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Services
{
    public class SeparationService : ISeparationService
    {
        public event EventHandler<SeparationEventArgs> SeparationEventOccured;
        public void UpdateSeparationEvents(List<ITrack> tracks)
        {
            throw new NotImplementedException();
        }
    }
}