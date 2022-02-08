using System.Text.Json.Serialization;
using OBilet.Core.Base;


namespace OBilet.Core.Sessions
{
    public class GetSessionResponse: BaseResponse
    {
        [JsonPropertyName("data")]
        public new Session? Data { get; set; }
    }
}
