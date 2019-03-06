using System.Runtime.Remoting.Channels;
using AirTrafficMonitor.Lib.EventArgs;

namespace AirTrafficMonitor.Lib.Interfaces
{
    public interface IRender
    {
        void RenderTrackings(object sender, TrackEventArgs e);
        void RenderSeparationEvents(object sender, SeparationEventArgs e);
    }
}