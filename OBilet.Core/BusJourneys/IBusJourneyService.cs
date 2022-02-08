namespace OBilet.Core.BusJourneys
{
    public interface IBusJourneyService
    {
        Task<List<BusJourney>> GetBusJourneys(GetBusJourneyRequest request);
    }
}
