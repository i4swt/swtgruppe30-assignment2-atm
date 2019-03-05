using System.Runtime.Remoting.Channels;
using AirTrafficMonitor.Lib.EventArgs;

namespace AirTrafficMonitor.Lib.Interfaces
{
    public interface IRender
    {
        void RenderTracks(object sender, TrackEventArgs e);
        void RenderSeparationEvents(object sender, SeparationEventArgs e);
    }
}