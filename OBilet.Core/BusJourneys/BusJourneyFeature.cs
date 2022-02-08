using System.Text.Json.Serialization;

namespace OBilet.Core.BusJourneys
{
    public class BusJourneyFeature
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("priority")]
        public int? Priority { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("is-promoted")]
        public bool IsPromoted { get; set; }

        [JsonPropertyName("back-color")]
        public string? BackColor { get; set; }

        [JsonPropertyName("fore-color")]
        public string? ForeColor { get; set; }
    }
}
