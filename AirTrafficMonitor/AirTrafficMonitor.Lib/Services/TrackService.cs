using System;
using System.Collections.Generic;
using AirTrafficMonitor.Lib.EventArgs;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Services
{
    public class TrackService : ITrackService
    {
        public event EventHandler<TrackEventArgs> TracksChanged;
        public void UpdateTracks(List<ITrack> tracks)
        {
            throw new NotImplementedException();
        }

        public List<ITrack> ConvertRawDataToTracks(string rawData)
        {
            throw new NotImplementedException();
        }
    }
}