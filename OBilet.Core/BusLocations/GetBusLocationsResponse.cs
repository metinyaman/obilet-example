using System.Text.Json.Serialization;
using OBilet.Core.Base;

namespace OBilet.Core.BusLocations
{
    public class GetBusLocationsResponse: BaseResponse
    {
        [JsonPropertyName("data")]
        public new List<BusLocation>? Data { get; set; }
    }
}
