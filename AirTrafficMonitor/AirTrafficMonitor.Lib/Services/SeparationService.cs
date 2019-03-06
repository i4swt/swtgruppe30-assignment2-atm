using System;
using System.Collections.Generic;
using AirTrafficMonitor.Lib.EventArgs;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Services
{
    public class SeparationService : ISeparationService
    {
        public HashSet<ISeparationEvent> GetAllSeparationEvents(HashSet<ITrack> trackings)
        {
            throw new NotImplementedException();
        }

        public HashSet<ISeparationEvent> GetNewSeparationEvents(HashSet<ISeparationEvent> allSeparationEvents, HashSet<ISeparationEvent> oldSeparationEvents)
        {
            throw new NotImplementedException();
        }

        public void LogSeparationEvents(HashSet<ISeparationEvent> separationEvents)
        {
            throw new NotImplementedException();
        }

        public HashSet<ISeparationEvent> UpdateSeparationEvents(HashSet<ISeparationEvent> allSeparationEvents, HashSet<ISeparationEvent> oldSeparationEvents)
        {
            throw new NotImplementedException();
        }
    }
}