using Newtonsoft.Json;

namespace EmployeeScheduler.Clients.Utils
{
    internal static class HttpClientExtensions
    {
        internal static async Task<T> ReadAsJsonAsync<T>(this HttpResponseMessage response)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            return Deserialize<T>(stream);
        }

        internal static async Task EnsureSucceeded(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                throw new HttpException(
                    response.RequestMessage.RequestUri?.OriginalString,
                    response.RequestMessage.Method.ToString(),
                    (int)response.StatusCode,
                    content ?? response.ReasonPhrase);
            }
        }

        internal static async Task<string> ToStringAsync(this Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        internal static T Deserialize<T>(this Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new InvalidOperationException("Cannot read the stream.");
            }

            using (var reader = new StreamReader(stream))
            {
                using (var jsonReader = new JsonTextReader(reader))
                {
                    return new JsonSerializer().Deserialize<T>(jsonReader);
                }
            }
        }
    }
}
