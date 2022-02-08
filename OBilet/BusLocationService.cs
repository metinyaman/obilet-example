using System.Text.Json;
using OBilet.Core.Base;
using OBilet.Core.BusLocations;

namespace OBilet
{
    public class BusLocationService : IBusLocationService
    {
        public async Task<List<BusLocation>> GetBusLocations(GetBusLocationsRequest request)
        {
            var url = Endpoints.GetLocation;
            
            var response = JsonSerializer.Deserialize<GetBusLocationsResponse>(await OBiletHttpMethods.Post(url, request)!);

            var result = response?.Data?.ToList();

            return await Task.FromResult(result ?? new List<BusLocation>());
        }
    }
}