namespace OBilet.Web.Models
{
    public class JourneyIndexViewModel
    {
        public int Id { get; set; }
        public int OriginLocationId { get; set; }
        public int DestinationLocationId { get; set; }


        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
    }
}
