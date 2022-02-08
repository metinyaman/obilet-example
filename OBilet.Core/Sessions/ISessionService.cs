namespace OBilet.Core.Sessions;

public interface ISessionService
{
    Task<GetSessionResponse?> GetSession(GetSessionRequest request);
}