using System.Text.Json.Serialization;
using OBilet.Core.Base;

namespace OBilet.Core.BusJourneys
{
    public class GetBusJourneyRequest: BaseRequest
    {
        [JsonPropertyName("data")]
        public new GetBusJourneyRequestData? Data { get; set; }
    }

    public class GetBusJourneyRequestData
    {
        [JsonPropertyName("origin-id")]
        public int OriginId { get; set; }

        [JsonPropertyName("destination-id")]
        public int DestinationId { get; set; }

        [JsonPropertyName("departure-date")]
        public DateTime DepartureDate { get; set; }
    }
}
