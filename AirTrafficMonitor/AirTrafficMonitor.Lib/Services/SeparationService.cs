using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using AirTrafficMonitor.Lib.EventArgs;
using AirTrafficMonitor.Lib.Interfaces;
using AirTrafficMonitor.Lib.Models;

namespace AirTrafficMonitor.Lib.Services
{
    public class SeparationService : ISeparationService
    {
        public SeparationService()
        {

        }

        public SeparationService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public HashSet<ISeparationEvent> GetAllSeparationEvents(HashSet<ITrack> trackings)
        {
            if(trackings==null) throw new ArgumentNullException();

            HashSet<ISeparationEvent> allSeparationsEvents = new HashSet<ISeparationEvent>();

            for (int i = 0; i < trackings.Count - 1; i++)
            {
                ITrack firstElement = trackings.ElementAt<ITrack>(i);

                for (int j = i + 1; j < trackings.Count; j++)
                {
                    ITrack secondElement = trackings.ElementAt<ITrack>(j);
                    if (RaiseSeparationEvent(firstElement, secondElement))
                    {
                        allSeparationsEvents.Add(new SeparationEvent(firstElement.Tag, secondElement.Tag,
                            System.DateTime.Now));
                    }
                }
            }

            return allSeparationsEvents;
        }

        public bool RaiseSeparationEvent(ITrack track1, ITrack track2)
        {
            //calculates both vertical and horizontal distances
            double verticalDistance = Math.Abs(track1.Coordinate.Z-track2.Coordinate.Z);
            double horizontalDistance =
                    (Math.Sqrt(Math.Pow(Math.Abs(track1.Coordinate.X - track2.Coordinate.X), 2) +
                               Math.Pow(Math.Abs(track1.Coordinate.Y - track2.Coordinate.Y), 2)));

            //Checks if separationevent should be raised
            if (verticalDistance < 300 && horizontalDistance < 5000)
                return true;
            else
                return false;
        }


        public HashSet<ISeparationEvent> GetNewSeparationEvents(HashSet<ISeparationEvent> allSeparationEvents, HashSet<ISeparationEvent> oldSeparationEvents)
        {
            if(allSeparationEvents==null || oldSeparationEvents == null) throw new ArgumentNullException();

            HashSet<ISeparationEvent> newSeparationEvents = new HashSet<ISeparationEvent>();

            foreach (var newSeparationEvent in allSeparationEvents)
            {
                bool _contains = false;

                foreach (var oldSeparationEvent in oldSeparationEvents)
                {
                    if ((newSeparationEvent.Tag1 == oldSeparationEvent.Tag1) &&
                        (newSeparationEvent.Tag2 == oldSeparationEvent.Tag2))
                    {
                        _contains = true;
                    }
                }
                if(!_contains)
                    newSeparationEvents.Add(newSeparationEvent);
            }

            return newSeparationEvents;
        }

        public void LogSeparationEvents(HashSet<ISeparationEvent> separationEvents)
        {
            foreach (var separationEvent in separationEvents)
            {
                string stringToLog = separationEvent.Timestamp.ToString() + " , " + separationEvent.Tag1 + " , " +
                                       separationEvent.Tag2;
;                _loggingService.Log(stringToLog);
            }
        }

        public HashSet<ISeparationEvent> UpdateSeparationEvents(HashSet<ISeparationEvent> allSeparationEvents, HashSet<ISeparationEvent> oldSeparationEvents)
        {
            if(allSeparationEvents==null ||oldSeparationEvents==null) throw new ArgumentNullException();

            HashSet<ISeparationEvent> updatedSeparationEvents = new HashSet<ISeparationEvent>();

            foreach (var oldSeparationEvent in oldSeparationEvents)
            {
                bool _contains = false;
                foreach (var newSeparationEvent in allSeparationEvents)
                {
                    if ((newSeparationEvent.Tag1 == oldSeparationEvent.Tag1) &&
                        (newSeparationEvent.Tag2 == oldSeparationEvent.Tag2))
                    {
                        _contains = true;
                    }
                }
                if (_contains)
                    updatedSeparationEvents.Add(oldSeparationEvent);
            }

            foreach(var newSeparationEvent in allSeparationEvents)
            {
                bool _contains = false;
                foreach (var oldSeparationEvent in oldSeparationEvents)
                {
                    if ((newSeparationEvent.Tag1 == oldSeparationEvent.Tag1) &&
                        (newSeparationEvent.Tag2 == oldSeparationEvent.Tag2))
                    {
                        _contains = true;
                    }
                }
                if (!_contains)
                    updatedSeparationEvents.Add(newSeparationEvent);
            }

            return updatedSeparationEvents;
        }

        private readonly ILoggingService _loggingService;
    }
}