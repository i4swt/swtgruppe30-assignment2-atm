namespace AirTrafficMonitor.Lib.Interfaces
{
    public interface IAirspace
    {
        ITwoDimensionalCoordinate SouthWestCorner { get; }
        ITwoDimensionalCoordinate NorthEastCorner { get; }
        int LowerAltitudeBoundary { get; }
        int UpperAltitudeBoundary { get; }
        
    }
}
