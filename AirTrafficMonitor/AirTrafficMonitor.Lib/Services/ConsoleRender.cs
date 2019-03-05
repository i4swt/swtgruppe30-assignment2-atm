using AirTrafficMonitor.Lib.EventArgs;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Services
{
    public class ConsoleRender : IRender
    {
        public void RenderTracks(object sender, TrackEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public void RenderSeparationEvents(object sender, SeparationEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}