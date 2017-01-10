using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI
{
    public class QMSApiClient
    {
        private readonly HttpClient _httpClient;
        public string token { get; set; }

        public string BaseURL { get; set; }

        public string REST_SERVICE_URL_PREFIX { get; set; }

        public QMSApiClient()
        {
            _httpClient = new HttpClient();
        }

        public void SetToken()
        {
            if (!string.IsNullOrWhiteSpace(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        private Uri CreateBaseURL()
        {
            var baseAddress = new Uri(BaseURL);
            return baseAddress;
        }

        private async Task<HttpClient> CreateHttpClient(Uri baseAddress, HttpClientHandler handler)
        {
            var client = new HttpClient(handler);
            client.BaseAddress = baseAddress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await Task.FromResult(client);
        }

        public async Task<HttpClient> CreateHttpClient(Uri baseAddress, string mimetype)
        {
            var client = new HttpClient(await CreateHttpClientHandler(baseAddress));
            client.BaseAddress = baseAddress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mimetype));

            return await Task.FromResult(client);
        }

        private async Task<HttpClientHandler> CreateHttpClientHandler(Uri baseAddress)
        {
            var cookieContainer = new CookieContainer();

            return await Task.FromResult(
                new HttpClientHandler()
                {
                    CookieContainer = cookieContainer,
                    AllowAutoRedirect = true
                }
                );
        }

        private async Task<HttpRequestMessage> CreateHttpGetRequest()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, REST_SERVICE_URL_PREFIX);

            return await Task.FromResult(request);
        }

        private async Task<HttpRequestMessage> CreateHttpPostRequest(String bodyContentToSend)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, REST_SERVICE_URL_PREFIX);

            request.Content = new StringContent(bodyContentToSend, Encoding.UTF8, "application/json");

            return await Task.FromResult(request);
        }

        private async Task<HttpRequestMessage> CreateHttpPutRequest(String bodyContentToSend)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, REST_SERVICE_URL_PREFIX);

            request.Content = new StringContent(bodyContentToSend, Encoding.UTF8, "application/json");

            return await Task.FromResult(request);
        }

        private async Task<HttpRequestMessage> CreateHttpDeleteRequest()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, REST_SERVICE_URL_PREFIX);

            return await Task.FromResult(request);
        }

        public async Task<HttpResponseMessage> GetResourceAsync()
        {
            var baseAddress = CreateBaseURL();
            using (var handler = await CreateHttpClientHandler(baseAddress))
            {
                using (var client = await CreateHttpClient(baseAddress, handler))
                {
                    HttpRequestMessage request = await CreateHttpGetRequest();

                    handler.AllowAutoRedirect = true;

                    HttpResponseMessage response = await client.SendAsync(request);

                    response.EnsureSuccessStatusCode();

                    return response;
                }
            }
        }

        public string GetJsonResponse()
        {
            return GetResourceAsync().Result.Content.ReadAsStringAsync().Result;
        }

        public string PostJsonResponse(string postBodyContent)
        {
            return PostResourceAsync(postBodyContent).Result.Content.ReadAsStringAsync().Result;
        }

        public string PutJsonResponse(string postBodyContent)
        {
            return PostResourceAsync(postBodyContent).Result.Content.ReadAsStringAsync().Result;
        }

        public string DeleteJsonResponse()
        {
            return DeleteResourceAsync().Result.Content.ReadAsStringAsync().Result;
        }

        public async Task<HttpResponseMessage> PostResourceAsync(String bodyContent)
        {
            var baseAddress = CreateBaseURL();
            using (var handler = await CreateHttpClientHandler(baseAddress))
            {
                using (var client = await CreateHttpClient(baseAddress, handler))
                {
                    HttpRequestMessage request = await CreateHttpPostRequest(bodyContent);

                    handler.AllowAutoRedirect = true;

                    HttpResponseMessage response = await client.SendAsync(request);

                    response.EnsureSuccessStatusCode();

                    return response;
                }
            }
        }

        public async Task<HttpResponseMessage> PutResourceAsync(String bodyContent)
        {
            var baseAddress = CreateBaseURL();
            using (var handler = await CreateHttpClientHandler(baseAddress))
            {
                using (var client = await CreateHttpClient(baseAddress, handler))
                {
                    HttpRequestMessage request = await CreateHttpPutRequest(bodyContent);

                    handler.AllowAutoRedirect = true;

                    HttpResponseMessage response = await client.SendAsync(request);

                    response.EnsureSuccessStatusCode();

                    return response;
                }
            }
        }

        public async Task<HttpResponseMessage> DeleteResourceAsync()
        {
            var baseAddress = CreateBaseURL();
            using (var handler = await CreateHttpClientHandler(baseAddress))
            {
                using (var client = await CreateHttpClient(baseAddress, handler))
                {
                    HttpRequestMessage request = await CreateHttpDeleteRequest();

                    handler.AllowAutoRedirect = true;

                    HttpResponseMessage response = await client.SendAsync(request);

                    response.EnsureSuccessStatusCode();

                    return response;
                }
            }
        }

        public async Task<Stream> GetResourceStreamAsync()
        {
            var baseAddress = CreateBaseURL();
            using (var handler = await CreateHttpClientHandler(baseAddress))
            {
                using (var client = await CreateHttpClient(baseAddress, handler))
                {

                    handler.AllowAutoRedirect = true;
                    HttpResponseMessage response = await client.GetAsync(REST_SERVICE_URL_PREFIX);

                    response.EnsureSuccessStatusCode();
                    ////TResult result = await response.Content.ReadAsStreamAsync();//.ReadAsAsync<TResult>();

                    return await response.Content.ReadAsStreamAsync();
                    //return response;
                }
            }
        }
    }
}
