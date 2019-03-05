using System;
using System.Collections.Generic;
using AirTrafficMonitor.Lib.EventArgs;

namespace AirTrafficMonitor.Lib.Interfaces
{
    public interface ITrackService
    {
        event EventHandler<TrackEventArgs> TracksChanged;
        void UpdateTracks(List<ITrack> tracks);
        List<ITrack> ConvertRawDataToTracks(string rawData);
    }
}