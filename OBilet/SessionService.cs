using System.Text.Json;
using OBilet.Core.Base;
using OBilet.Core.Sessions;

namespace OBilet
{
    public class SessionService : ISessionService
    {
        public async Task<GetSessionResponse?> GetSession(GetSessionRequest request)
        {
            var url = Endpoints.GetSessionUrl;

            var result = JsonSerializer.Deserialize<GetSessionResponse>(await OBiletHttpMethods.Post(url, request)!);

            return result;
        }
    }
}

