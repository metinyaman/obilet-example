using System.Text.Json.Serialization;
using OBilet.Core.Journeys;

namespace OBilet.Core.BusJourneys
{
    public class BusJourney
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("partner-id")]
        public int PartnerId { get; set; }

        [JsonPropertyName("partner-name")]
        public string? PartnerName { get; set; }

        [JsonPropertyName("route-id")]
        public int RouteId { get; set; }

        [JsonPropertyName("bus-type")]
        public string? BusType { get; set; }

        [JsonPropertyName("total-seats")]
        public int TotalSeats { get; set; }

        [JsonPropertyName("available-seats")]
        public int AvailableSeats { get; set; }

        [JsonPropertyName("journey")]
        public Journey? Journey { get; set; }

        [JsonPropertyName("features")]
        public List<BusJourneyFeature>? Features { get; set; }

        [JsonPropertyName("origin-location")]
        public string? OriginLocation { get; set; }

        [JsonPropertyName("destination-location")]
        public string? DestinationLocation { get; set; }

        [JsonPropertyName("is-active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("origin-location-id")]
        public int OriginLocationId { get; set; }

        [JsonPropertyName("destination-location-id")]
        public int DestinationLocationId { get; set; }

        [JsonPropertyName("is-promoted")]
        public bool IsPromoted { get; set; }

        [JsonPropertyName("cancellation-offset")]
        public int? CancellationOffset { get; set; }

        [JsonPropertyName("has-bus-shuttle")]
        public bool HasBusShuttle { get; set; }

        [JsonPropertyName("disable-sales-without-gov-id")]
        public bool DisableSalesWithoutGovId { get; set; }

        [JsonPropertyName("display-offset")]
        public TimeSpan DisplayOffset { get; set; }

        [JsonPropertyName("partner-rating")]
        public double? PartnerRating { get; set; }
    }
}