using System.Text.Json.Serialization;
using OBilet.Core.Base;

namespace OBilet.Core.BusLocations
{
    public class GetBusLocationsRequest: BaseRequest
    {
        [JsonPropertyName("data")]
        public new string? Data { get; set; }
    }
}
