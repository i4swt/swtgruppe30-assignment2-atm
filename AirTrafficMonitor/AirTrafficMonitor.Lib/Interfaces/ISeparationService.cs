using System;
using System.Collections.Generic;
using AirTrafficMonitor.Lib.EventArgs;

namespace AirTrafficMonitor.Lib.Interfaces
{
    public interface ISeparationService
    {
        HashSet<ISeparationEvent> GetAllSeparationEvents(HashSet<ITrack> trackings);
        HashSet<ISeparationEvent> GetNewSeparationEvents(HashSet<ISeparationEvent> allSeparationEvents, HashSet<ISeparationEvent> oldSeparationEvents);
        void LogSeparationEvents(HashSet<ISeparationEvent> separationEvents);

        HashSet<ISeparationEvent> UpdateSeparationEvents(HashSet<ISeparationEvent> allSeparationEvents, HashSet<ISeparationEvent> oldSeparationEvents);
    }
}