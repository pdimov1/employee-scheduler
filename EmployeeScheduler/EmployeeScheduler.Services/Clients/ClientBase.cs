using EmployeeScheduler.Clients.Utils;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace EmployeeScheduler.Clients.Clients
{
    public abstract class ClientBase 
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ShiftClient> _logger;

        public ClientBase(IHttpClientFactory httpClientFactory, ILogger<ShiftClient> logger)
        {
            _httpClient = httpClientFactory.CreateClient("SchedulerApi");
            _logger = logger;
        }

        protected async Task<T> GetAsync<T>(string url, IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            try
            {
                using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    SetHttpRequestMessageHeaders(httpRequestMessage, headers);

                    var response = await _httpClient.SendAsync(httpRequestMessage);

                    await response.EnsureSucceeded();

                    return await response.ReadAsJsonAsync<T>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GET request: [{url}] - {Message}", url, ex.Message);

                throw;
            }
        }

        protected async Task SendMessageAsync(
            string url,
            object request,
            HttpMethod httpMethod,
            IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            try
            {
                using (var httpRequestMessage = new HttpRequestMessage(httpMethod, url))
                {
                    httpRequestMessage.Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                        "application/json");
                    ;
                    SetHttpRequestMessageHeaders(httpRequestMessage, headers);

                    var response = await _httpClient.SendAsync(httpRequestMessage);
                    await response.EnsureSucceeded();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in {Method} request: [{url}] - {Message}", httpMethod.Method, url, ex.Message);

                throw;
            }
        }

        private void SetHttpRequestMessageHeaders(HttpRequestMessage httpRequestMessage,
            IEnumerable<KeyValuePair<string, string>> headers)
        {
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> kvp in headers)
                {
                    httpRequestMessage.Headers.Add(kvp.Key, kvp.Value);
                }

            }
        }
    }
}
