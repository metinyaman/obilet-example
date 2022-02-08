using System.Text.Json;
using OBilet.Core.Base;
using OBilet.Core.BusJourneys;

namespace OBilet
{
    public class BusJourneyService : IBusJourneyService
    {
        public async Task<List<BusJourney>> GetBusJourneys(GetBusJourneyRequest request)
        {
            var url = Endpoints.GetBusJourneys;

            var response = JsonSerializer.Deserialize<GetBusJourneysResponse>(await OBiletHttpMethods.Post(url, request)!);

            var result = response?.Data?.OrderBy(r => r.Journey?.Departure).ToList();

            return await Task.FromResult(result ?? new List<BusJourney>());
        }
    }
}