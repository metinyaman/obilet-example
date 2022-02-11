using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using OBilet.Core.Base;

namespace OBilet
{
    public class OBiletHttpMethods
    {
        public static async Task<string> Post(string url, object request)
        {
            var client = new HttpClient { BaseAddress = new Uri(Endpoints.BaseUrl) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {Endpoints.Token}");

            var json = JsonSerializer.Serialize(request);

            var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
