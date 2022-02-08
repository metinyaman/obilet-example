using System.Text.Json.Serialization;

namespace OBilet.Core.Base
{
    public class BaseResponse
    {

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("data")]
        public object? Data { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("user-message")]
        public string? UserMessage { get; set; }

        [JsonPropertyName("api-request-id")]
        public string? ApiRequestId { get; set; }

        [JsonPropertyName("controller")]
        public string? Controller { get; set; }
    }
}
