using System.Text.Json.Serialization;

namespace OBilet.Core.BusLocations
{
    public class BusLocation
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("parent-id")]
        public int? ParentId { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("geo-location")]
        public GeoLocation? GeoLocation { get; set; }

        [JsonPropertyName("tz-code")]
        public string? TzCode { get; set; }

        [JsonPropertyName("weather-code")]
        public string? WeatherCode { get; set; }

        [JsonPropertyName("rank")]
        public int? Rank { get; set; }

        [JsonPropertyName("reference-code")]
        public string? ReferenceCode { get; set; }

        [JsonPropertyName("keywords")]
        public string? Keywords { get; set; }
    }

    public class GeoLocation
    {
        [JsonPropertyName("zoom")]
        public int? Zoom { get; set; }

        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }

    }
}
