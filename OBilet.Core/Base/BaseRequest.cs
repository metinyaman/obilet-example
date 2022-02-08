using System.Text.Json.Serialization;
using OBilet.Core.Sessions;

namespace OBilet.Core.Base
{
    public class BaseRequest
    {
        [JsonPropertyName("data")]
        public object? Data { get; set; }

        [JsonPropertyName("device-session")]
        public Session? DeviceSession { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("language")] 
        public string Language { get; set; } = "en-EN";
    }
}
