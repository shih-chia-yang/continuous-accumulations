using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace marketplace.api.External
{
    public class PurgomalumClient
    {
        private readonly HttpClient _httpClient;

        private readonly string _baseUrl = "https://www.purgomalum.com/service/containsprofanity";
        public PurgomalumClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CheckForProfanity(string text)
        {
            var result = await _httpClient.GetAsync(
                QueryHelpers.AddQueryString(_baseUrl, "text", text));
            
            var value = await result.Content.ReadAsStringAsync();
            return bool.Parse(value);
        }
    }
}