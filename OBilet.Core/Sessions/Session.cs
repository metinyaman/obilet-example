using System.Text.Json.Serialization;

namespace OBilet.Core.Sessions
{
    public class Session
    {
        [JsonPropertyName("session-id")]
        public string? SessionId { get; set; }

        [JsonPropertyName("device-id")]
        public string? DeviceId { get; set; }
    }
}
