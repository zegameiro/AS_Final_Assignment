using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;

namespace Piranha.Events
{
    public class NotificationService
    {
        private readonly HttpClient _httpClient;

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task NotifyAsync(string callbackUrl, object payload)
        {
            var jsonContent = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(callbackUrl, content); 
        }
    }
}