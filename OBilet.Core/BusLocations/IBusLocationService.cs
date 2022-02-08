namespace OBilet.Core.BusLocations
{
    public interface IBusLocationService
    {
        Task<List<BusLocation>> GetBusLocations (GetBusLocationsRequest request);
    }
}
