namespace AirTrafficMonitor.Lib.Interfaces
{
    public interface IAirTrafficMonitorFactory
    {
        ISeparationService SeparationService { get; set; }
        ITrackingService TrackingService { get; set; }
        IAirspaceService AirspaceService { get; set; }
        IAirspace Airspace { get; set; }
    }
}