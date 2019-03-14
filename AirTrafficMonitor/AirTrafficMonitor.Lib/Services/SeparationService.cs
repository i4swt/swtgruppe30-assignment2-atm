using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AirTrafficMonitor.Lib.Interfaces;
using AirTrafficMonitor.Lib.Models;

namespace AirTrafficMonitor.Lib.Services
{
    public class SeparationService : ISeparationService
    {

        public SeparationService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        //Takes a set of trackings and finds all separationevents - then returns it.
        public HashSet<ISeparationEvent> GetAllSeparationEvents(HashSet<ITrack> trackings)
        {
            if(trackings==null) throw new ArgumentNullException();

            HashSet<ISeparationEvent> allSeparationsEvents = new HashSet<ISeparationEvent>();

            //Compares every element to all other elements.
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

        //Takes two tracks and returns true, if they make a separationevent. Else returns false.
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

        //Takes two sets of separationevents. Returns a new set with all separationevents that exists in the first set, but not in the second set.
        //Timestamps are ignored, so if the old event still exists, it is not added to the returned set.
        public HashSet<ISeparationEvent> GetNewSeparationEvents(HashSet<ISeparationEvent> allSeparationEvents, HashSet<ISeparationEvent> oldSeparationEvents)
        {
            if(allSeparationEvents==null || oldSeparationEvents == null) throw new ArgumentNullException();

            HashSet<ISeparationEvent> newSeparationEvents = new HashSet<ISeparationEvent>();

            foreach (var newSeparationEvent in allSeparationEvents)
            {
                bool _contains = false;

                foreach (var oldSeparationEvent in oldSeparationEvents)
                {
                    //The event is only considered new, if the tags doesn't already exist. Timestamps are ignored.
                    if ((newSeparationEvent.Tag1 == oldSeparationEvent.Tag1) &&
                        (newSeparationEvent.Tag2 == oldSeparationEvent.Tag2))
                    {
                        _contains = true;
                    }
                }
                //If the separationevent did not exist in the old set, it is added to the returned set
                if(!_contains)
                    newSeparationEvents.Add(newSeparationEvent);
            }

            return newSeparationEvents;
        }

        //Converts the list of separationevents to separate strings. Calls Log() for every element.
        public void LogSeparationEvents(HashSet<ISeparationEvent> separationEvents)
        {
            foreach (var separationEvent in separationEvents)
            {
                string stringToLog = separationEvent.Timestamp.ToString(CultureInfo.InvariantCulture) + " , " + separationEvent.Tag1 + " , " +
                                       separationEvent.Tag2;
;                _loggingService.Log(stringToLog);
            }
        }


        //Takes two sets of separationevents. Returns a new set that contains the old separationevents, that are also in the new set.
        //Also returns the events, that are in the new but not in the old.
        //Does not return elements, that are in the old, but not in the new set.
        public HashSet<ISeparationEvent> UpdateSeparationEvents(HashSet<ISeparationEvent> allSeparationEvents, HashSet<ISeparationEvent> oldSeparationEvents)
        {
            if(allSeparationEvents==null ||oldSeparationEvents==null) throw new ArgumentNullException();

            HashSet<ISeparationEvent> updatedSeparationEvents = new HashSet<ISeparationEvent>();

            //If a separation event exists in both the old and new set, the old event is added to the returned set.
            //Thereby we dont return elements, that exist in the old set but not in the new set.
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

            //If an element exists in the new set but not in the old, this element is added to the returned set.
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