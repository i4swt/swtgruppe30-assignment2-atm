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

            //Initialize
            _trackings = new HashSet<ITrack>();
            _separationEvents = new HashSet<ISeparationEvent>();
        }

        private void TransponderReceiver_DataReady(object sender, RawTransponderDataEventArgs e)
        {
            //All to do with tracking and updates
            HashSet<ITrack> newTrackings = _trackingService.CreateTrackings(e.TransponderData);

            HashSet<ITrack> filteredTrackings = _airspaceService.GetTrackingsInAirspace(newTrackings, _airspace);

            _trackings = _trackingService.UpdateTrackings(filteredTrackings, _trackings);

            OnTrackingsChanged();

            //All to do with separations
            HashSet<ISeparationEvent> allSeparationEvents = _separationService.GetAllSeparationEvents(_trackings);

            HashSet<ISeparationEvent> newSeparationEvents =
                _separationService.GetNewSeparationEvents(allSeparationEvents, _separationEvents);

            _separationService.LogSeparationEvents(newSeparationEvents);

            _separationEvents = _separationService.UpdateSeparationEvents(allSeparationEvents, _separationEvents);

            OnSeparationEventChanged();
        }

        private void OnTrackingsChanged()
        {
            TrackingsChanged?.Invoke(this, new TrackEventArgs(_trackings));
        }

        private void OnSeparationEventChanged()
        {
            SeparationEventsChanged?.Invoke(this, new SeparationEventArgs(_separationEvents));
        }
    }
}
