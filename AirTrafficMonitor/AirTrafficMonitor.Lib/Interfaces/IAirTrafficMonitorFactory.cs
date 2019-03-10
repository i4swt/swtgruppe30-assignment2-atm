namespace AirTrafficMonitor.Lib.Interfaces
{
    public interface IAirTrafficMonitorFactory
    {
        ISeparationService SeparationService { get; }
        ITrackingService TrackingService { get; }
        IAirspaceService AirspaceService { get; }
        IAirspace Airspace { get; }
    }
}