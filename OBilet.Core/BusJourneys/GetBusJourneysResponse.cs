using System.Text.Json.Serialization;
using OBilet.Core.Base;

namespace OBilet.Core.BusJourneys
{
    public class GetBusJourneysResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public new List<BusJourney>? Data { get; set; }

        [JsonPropertyName("controller")]
        public new string Controller { get; set; } = "Journey";
    }
}
