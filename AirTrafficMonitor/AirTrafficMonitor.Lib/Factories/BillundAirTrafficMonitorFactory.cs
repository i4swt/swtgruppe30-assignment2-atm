using AirTrafficMonitor.Lib.Interfaces;
using AirTrafficMonitor.Lib.Models;
using AirTrafficMonitor.Lib.Services;

namespace AirTrafficMonitor.Lib.Factories
{
    public class BillundAirTrafficMonitorFactory : IAirTrafficMonitorFactory
    {
        public ISeparationService SeparationService => new SeparationService(new LoggingService());

        public ITrackingService TrackingService => new TrackingService();

        public IAirspaceService AirspaceService => new AirspaceService();

        /// <summary>
        /// Creates an airspace in the center of the area from which the transponder emits data
        /// Southwest corner: 10.000, 10.000. Airspace fixed x/y size of 80.000. X => 10.000 to 90.000, Y => 10.000 to 90.000
        /// </summary>
        public IAirspace Airspace => new Airspace(new TwoDimensionalCoordinate(10000, 10000));
    }
}