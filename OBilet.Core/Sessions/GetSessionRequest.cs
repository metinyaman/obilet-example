using System.Text.Json.Serialization;

namespace OBilet.Core.Sessions
{
    public class GetSessionRequest
    {
        [JsonPropertyName("type")]
        public int Type { get; set; } = 7;

        [JsonPropertyName("connection")]
        public Connection? Connection { get; set; }

        [JsonPropertyName("application")]
        public Application? Application { get; set; }

        [JsonPropertyName("browser")]
        public Browser? Browser { get; set; }
    }

    public class Application
    {
        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.0.0.0";

        [JsonPropertyName("equipment-id")]
        public string EquipmentId { get; set; } = "distribution";
    }

    public class Connection
    {
        [JsonPropertyName("ip-address")]
        public string? IdAddress { get; set; }

        [JsonPropertyName("port")]
        public int Port { get; set; }
    }

    public class Browser
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("version")]
        public string? Version { get; set; }
    }
}
