using System;
using System.Collections.Generic;
using System.Globalization;
using AirTrafficMonitor.Lib.Interfaces;
using AirTrafficMonitor.Lib.Models;

namespace AirTrafficMonitor.Lib.Services
{
    public class TrackingService : ITrackingService
    {
        /// <summary>
        /// Converts raw transponder data into a hashset of trackings
        /// </summary>
        /// <param name="rawData">Raw data from transponder</param>
        /// <returns>A hash set containing ITrack objects</returns>
        public HashSet<ITrack> CreateTrackings(List<string> rawData)
        {
            if (rawData == null) throw new ArgumentNullException();

            HashSet<ITrack> set = new HashSet<ITrack>();

            foreach (var trackingData in rawData)
            {
                if (IsValidTrackFormat(trackingData)) set.Add(new Track(trackingData));
            }

            return set;
        }

        /// <summary>
        /// Determines if the data is eligible for instantiation of an ITrack object
        /// </summary>
        /// <param name="data">Raw data of one tracking received from the transponder</param>
        /// <returns>True if the data is eligible for instantiation of an ITrack object</returns>
        private bool IsValidTrackFormat(string trackingData)
        {
            // Example data: "ATR423;39045;12932;14000;20151006213456789”
            var data = trackingData.Split(';');

            // Number of elements
            if (data.Length != 5) return false;

            // Tag length
            if (data[0].Length != 6) return false;

            // Check for integers (int.Parse throws Exception on null argument)
            try
            {
                int.Parse(data[1]);
                int.Parse(data[2]);
                int.Parse(data[3]);
            }
            catch (Exception)
            {
                return false;
            }

            // Check for date length
            if (data[4].Length != 17) return false;

            // Check for date format (ParseExact throws Exception on failure)
            try
            {
                DateTime.ParseExact(data[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Updates speed, bearing etc. of the newTrackings  
        /// </summary>
        /// <param name="newTrackings">Hash set of the newest ITrack objects</param>
        /// <param name="oldTrackings">Hash set of the old ITrack objects</param>
        /// <returns>A hash set of updated ITrack objects</returns>
        public HashSet<ITrack> UpdateTrackings(HashSet<ITrack> newTrackings, HashSet<ITrack> oldTrackings)
        {
            if (newTrackings == null || oldTrackings == null) throw new ArgumentNullException();

            // Update new set of trackings with data from the old set of tracking
            foreach (var newTracking in newTrackings)
            {
                // There is and old tracking which can be used to update this new tracking
                if (oldTrackings.Contains(newTracking))
                {
                    foreach (var oldTracking in oldTrackings)
                    {
                        if (oldTracking.Equals(newTracking)) newTracking.Update(oldTracking);
                    }
                }
            }

            return newTrackings;
        }

    }

}