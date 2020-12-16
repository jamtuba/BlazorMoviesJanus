using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers
{
    public class HttpService : IHttpService
    {
        private readonly HttpClientWithToken _httpClientWithToken;
        private readonly HttpClientWithoutToken _httpClientWithoutToken;

        private JsonSerializerOptions DefaultJsonSerializerOptions =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public HttpService(HttpClientWithToken httpClientWithToken, HttpClientWithoutToken httpClientWithoutToken)
        {
            _httpClientWithToken = httpClientWithToken;
            _httpClientWithoutToken = httpClientWithoutToken;
        }

        private HttpClient GetHttpClient(bool includeToken = true)
        {
            if (includeToken)
            {
                return _httpClientWithToken.HttpClient;
            }
            else
            {
                return _httpClientWithoutToken.HttpClient;
            }
            
        }

        public async Task<HttpResponseWrapper<T>> Get<T>(string url, bool includeToken = true)
        {
            var _httpClient = GetHttpClient(includeToken);
            var responseHTTP = await _httpClient.GetAsync(url);

            if (responseHTTP.IsSuccessStatusCode)
            {
                var response = await Deserialize<T>(responseHTTP, DefaultJsonSerializerOptions);
                return new HttpResponseWrapper<T>(response, true, responseHTTP);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, false, responseHTTP);
            }
        }


        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T data)
        {
            var _httpClient = GetHttpClient();
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, stringContent);
            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T data)
        {
            var _httpClient = GetHttpClient();
            //var dataJson = JsonSerializer.Serialize(data);
            //var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsJsonAsync(url, data);
            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data, bool includeToken = true)
        {
            var _httpClient = GetHttpClient(includeToken);
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseDeserialized = await Deserialize<TResponse>(response, DefaultJsonSerializerOptions);
                return new HttpResponseWrapper<TResponse>(responseDeserialized, true, response);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, false, response);
            }
        }

        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, options);
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var _httpClient = GetHttpClient();
            var responseHTTP = await _httpClient.DeleteAsync(url);

            return new HttpResponseWrapper<object>(null, responseHTTP.IsSuccessStatusCode, responseHTTP);

        }
    }
}
