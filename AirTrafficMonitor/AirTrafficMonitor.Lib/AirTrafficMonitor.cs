using AirTrafficMonitor.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Lib.EventArgs;
using TransponderReceiver;

namespace AirTrafficMonitor.Lib
{
    public class AirTrafficMonitor
    {
        public event EventHandler<TrackEventArgs> TrackingsChanged; 
        public event EventHandler<SeparationEventArgs> SeparationEventsChanged;

        private ISeparationService _separationService;
        private ITrackingService _trackingService;
        private IAirspaceService _airspaceService;
        private IAirspace _airspace;
        private HashSet<ITrack> _trackings;
        private HashSet<ISeparationEvent> _separationEvents;

        public AirTrafficMonitor(IAirTrafficMonitorFactory factory, ITransponderReceiver receiver)
        {
            _separationService = factory.SeparationService;
            _trackingService = factory.TrackingService;
            _airspaceService = factory.AirspaceService;
            _airspace = factory.Airspace;

            //Subscribe to events. 
            receiver.TransponderDataReady += TransponderReceiver_DataReady;
        }

        private void TransponderReceiver_DataReady(object sender, RawTransponderDataEventArgs e)
        {
            HashSet<ITrack> newTrackings = _trackingService.CreateTrackings(e.TransponderData);

            HashSet<ITrack> filteredTrackings = _airspaceService.GetTrackingsInAirspace(newTrackings, _airspace);

            _trackings = _trackingService.UpdateTrackings(filteredTrackings, _trackings);

            EventHandler<TrackEventArgs> handler = TrackingsChanged;
            if (TrackingsChanged != null)
            {
                handler(this, new TrackEventArgs(_trackings));
            }


        }
    }
}
