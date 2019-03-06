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
        event EventHandler<SeparationEventArgs> SeparationEventChanged;

        private IAirspace _airspace;
        private ITrackService _trackService;
        private ISeparationService _separationService;

        public AirTrafficMonitor(
            IAirspace airspace, 
            ITrackService trackService, 
            ISeparationService separationService, 
            ITransponderReceiver transponderReceiver)
        {
            _airspace = airspace;
            _trackService = trackService;
            _separationService = separationService;
            transponderReceiver.TransponderDataReady += TransponderReceiver_DataReady;
        }

        private void TransponderReceiver_DataReady(object sender, RawTransponderDataEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
