using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Middlewares
{
    public class QMSApiClient 
    {
        private readonly HttpClient _httpClient;
        public string token { get; set; }

        public string BaseURL { get; set; }

        public string REST_SERVICE_URL_PREFIX { get; set; }

        public QMSApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private void SetToken()
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

        #region RC1 not supported code. need to update it.
        //public async Task<Stream> GetStreamAsync(string path)
        //{
        //    SetToken();

        //    HttpResponseMessage response = await _httpClient.GetAsync(path).ConfigureAwait(false);
        //    if (response.StatusCode == HttpStatusCode.OK)
        //    {
        //        return response.Content.ReadAsStreamAsync().Result;
        //    }
        //    else
        //    {
        //        string jsonResult = response.Content.ReadAsStringAsync().Result;
        //        throw CreateException<string>(path, response, null, jsonResult, HttpMethod.Get);
        //    }
        //}

        //public async Task<T> GetJsonAsync<T>(string path, object bootstrapContext)
        //{
        //    SetToken(bootstrapContext);

        //    HttpResponseMessage response = await _httpClient.GetAsync(path).ConfigureAwait(false);
        //    string jsonResult = response.Content.ReadAsStringAsync().Result;
        //    if (response.StatusCode == HttpStatusCode.OK)
        //    {
        //        T resultObject = JsonConvert.DeserializeObject<T>(jsonResult);
        //        return resultObject;
        //    }
        //    else
        //    {
        //        throw CreateException<string>(path, response, null, jsonResult, HttpMethod.Get);
        //    }
        //}

        //public async Task<HttpResponseMessage> PutJsonAsync<T>(string path, T entity)
        //{
        //    SetToken();

        //    HttpResponseMessage response = await _httpClient.PutAsJsonAsync(path, entity).ConfigureAwait(false);
        //    string jsonResult = response.Content.ReadAsStringAsync().Result;
        //    Logger.Trace(String.Format("URL:{0}|RequestBody:{1}", path, JsonConvert.SerializeObject(entity)));
        //    Logger.Trace(String.Format("ServerResponse:{0}", jsonResult));
        //    if (response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.OK)
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        throw CreateException<T>(path, response, entity, jsonResult, HttpMethod.Put);
        //    }

        //}

        //public async Task<HttpResponseMessage> DeleteJsonAsync(string path)
        //{
        //    SetToken();

        //    HttpResponseMessage response = await _httpClient.DeleteAsync(path).ConfigureAwait(false);
        //    string jsonResult = response.Content.ReadAsStringAsync().Result;
        //    Logger.Trace(String.Format("URL:{0}", path));
        //    Logger.Trace(String.Format("ServerResponse:{0}", jsonResult));
        //    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        throw CreateException<string>(path, response, null, jsonResult, HttpMethod.Delete);
        //    }

        //}

        //public async Task<ResponseType> PostJsonAsync<RequestType, ResponseType>(string path, RequestType entity)
        //{
        //    SetToken();

        //    var response = await _httpClient.PostAsJsonAsync(path, entity).ConfigureAwait(false);
        //    string jsonResult = response.Content.ReadAsStringAsync().Result;
        //    Logger.Trace(String.Format("URL:{0}|RequestBody:{1}", path, JsonConvert.SerializeObject(entity)));
        //    Logger.Trace(String.Format("ServerResponse:{0}", jsonResult));
        //    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
        //    {
        //        var resultObject = JsonConvert.DeserializeObject<ResponseType>(jsonResult);
        //        return resultObject;
        //    }
        //    else
        //    {
        //        throw CreateException<RequestType>(path, response, entity, jsonResult, HttpMethod.Post);
        //    }
        //}

        //public async Task<Stream> PostStreamAsync<T>(string path, T entity)
        //{
        //    SetToken();

        //    var response = await _httpClient.PostAsJsonAsync(path, entity).ConfigureAwait(false);

        //    if (response.StatusCode == HttpStatusCode.OK)
        //    {
        //        return response.Content.ReadAsStreamAsync().Result; ;
        //    }
        //    else
        //    {
        //        string jsonResult = response.Content.ReadAsStringAsync().Result;
        //        throw CreateException<string>(path, response, null, jsonResult, HttpMethod.Post);
        //    }
        //}

        //public async Task<ResponseType> PostMultipartAsync<ResponseType>(string path, HttpContent content)
        //{
        //    SetToken();

        //    var response = await _httpClient.PostAsync(path, content).ConfigureAwait(false);
        //    string jsonResult = response.Content.ReadAsStringAsync().Result;
        //    if (response.StatusCode == HttpStatusCode.OK)
        //    {
        //        var resultObject = JsonConvert.DeserializeObject<ResponseType>(jsonResult);
        //        return resultObject;
        //    }
        //    else
        //    {
        //        throw CreateException(path, response, content, jsonResult, HttpMethod.Post);
        //    }
        //}


        #region Exception Helper Methods

        /// <summary>
        /// Creates the appropriate exception based on the HttpStatusCode from the server.
        /// </summary>
        /// <typeparam name="RequestBodyType">The type of the Request body type.</typeparam>
        /// <param name="path">The path.</param>
        /// <param name="response">The response.</param>
        /// <param name="requestBody">The request body.</param>
        /// <param name="jsonResult">The json result.</param>
        /// <param name="httpMethod">The HTTP method used in the request.</param>
        /// <returns></returns>
        //private Exception CreateException<RequestBodyType>(string path, HttpResponseMessage response, RequestBodyType requestBody, string jsonResult, HttpMethod httpMethod)
        //{
        //    Logger.Debug(string.Join("\r\n", ClaimsPrincipal.Current.Identities.First().Claims));
        //    Logger.Debug(httpMethod);

        //    if (response.StatusCode == HttpStatusCode.BadRequest)
        //    {
        //        return CreateEntityMissingPropertyException<RequestBodyType>(path, jsonResult, requestBody);
        //    }
        //    else if (response.StatusCode == HttpStatusCode.NotFound)
        //    {
        //        return CreateEntityNotFoundException<RequestBodyType>(path, jsonResult, requestBody);
        //    }
        //    else if (response.StatusCode == HttpStatusCode.Gone)
        //    {
        //        return CreateEntityGoneException<RequestBodyType>(path, jsonResult, requestBody);
        //    }
        //    else if (response.StatusCode == HttpStatusCode.Unauthorized)
        //    {
        //        return CreateUnauthenticatedException<RequestBodyType>(path, jsonResult, requestBody);
        //    }
        //    else if (response.StatusCode == HttpStatusCode.Conflict)
        //    {
        //        return CreateDuplicateEntityException<RequestBodyType>(path, jsonResult, requestBody, httpMethod);
        //    }
        //    else if (response.StatusCode == HttpStatusCode.PreconditionFailed)
        //    {
        //        return CreateConcurrencyException(path, jsonResult, requestBody);
        //    }
        //    else if (response.StatusCode == HttpStatusCode.NotAcceptable)
        //    {
        //        return CreateNotAcceptableException<RequestBodyType>(path, jsonResult, requestBody);
        //    }
        //    else
        //    {
        //        return CreateInvalidServerResponseException<RequestBodyType>(path, jsonResult, requestBody, response);
        //    }
        //}

        //private Exception CreateNotAcceptableException<T>(string path, string jsonResult, object requestBody)
        //{
        //    string jsonRequest = JsonConvert.SerializeObject(requestBody);
        //    string exceptionMessage = String.Format("API returned a NOT ACCEPTABLE response. Path: {0}{1}; Request Body: {2}; Server message: {3}", _httpClient.BaseAddress, path, jsonRequest, GetServerMessage(jsonResult));
        //    var exception = new NotAcceptableException(JsonConvert.DeserializeObject<ApiErrorResponse>(jsonResult).Message);
        //    Logger.DebugException(exceptionMessage, exception);
        //    return exception;
        //}

        //private Exception CreateDuplicateEntityException<T1>(string path, string jsonResult, object requestBody, HttpMethod httpMethod)
        //{
        //    string jsonRequest = JsonConvert.SerializeObject(requestBody);
        //    string exceptionMessage = String.Format("API returned a CONFLICT response. Path: {0}{1}, RequestBody: {2} Server message: {3}", _httpClient.BaseAddress, path, jsonRequest, GetServerMessage(jsonResult));

        //    Exception exception = null;

        //    if (httpMethod == HttpMethod.Post)
        //    {
        //        exception = new CreateDuplicateEntityException(exceptionMessage);
        //    }
        //    else if (httpMethod == HttpMethod.Put)
        //    {
        //        exception = new UpdateDuplicateEntityException(exceptionMessage);
        //    }
        //    else if (httpMethod == HttpMethod.Delete)
        //    {
        //        exception = new DeleteDuplicateEntityException(exceptionMessage);
        //    }

        //    Logger.DebugException(exceptionMessage, exception);
        //    return exception;
        //}

        //private UnauthenticatedException CreateUnauthenticatedException<RequestObjectType>(string path, string jsonResult, RequestObjectType requestBody)
        //{
        //    string jsonRequest = JsonConvert.SerializeObject(requestBody);
        //    string exceptionMessage = String.Format("API returned an Unauthenticated response. Path: {0}{1}; Request Body: {2}; Server message: {3}", _httpClient.BaseAddress, path, jsonRequest, GetServerMessage(jsonResult));
        //    var exception = new UnauthenticatedException(exceptionMessage);
        //    Logger.DebugException(exceptionMessage, exception);
        //    return exception;
        //}

        //private InvalidServerResponseException CreateInvalidServerResponseException<RequestObjectType>(string path, string jsonResult, RequestObjectType requestBody, HttpResponseMessage response)
        //{
        //    string jsonRequest = JsonConvert.SerializeObject(requestBody);
        //    string exceptionMessage = String.Format("API returned an unexpected response {0}. Path: {1}{2}; Request Body: {3}; Server message: {4}", response.StatusCode, _httpClient.BaseAddress, path, jsonRequest, GetServerMessage(jsonResult));
        //    InvalidServerResponseException exception = new InvalidServerResponseException(exceptionMessage);
        //    Logger.ErrorException(exceptionMessage, exception);
        //    return exception;
        //}

        //private EntityNotFoundException CreateEntityNotFoundException<RequestObjectType>(string path, string jsonResult, RequestObjectType requestBody)
        //{
        //    string jsonRequest = JsonConvert.SerializeObject(requestBody);
        //    string exceptionMessage = String.Format("API returned a NOT FOUND response. Path: {0}{1}; Request Body: {2}; Server message: {3}", _httpClient.BaseAddress, path, jsonRequest, GetServerMessage(jsonResult));
        //    EntityNotFoundException exception = new EntityNotFoundException(exceptionMessage);
        //    Logger.DebugException(exceptionMessage, exception);
        //    return exception;
        //}

        //private EntityGoneException CreateEntityGoneException<RequestObjectType>(string path, string jsonResult, RequestObjectType requestBody)
        //{
        //    string jsonRequest = JsonConvert.SerializeObject(requestBody);
        //    string exceptionMessage = String.Format("API returned a GONE response. Path: {0}{1}; Request Body: {2}; Server message: {3}", _httpClient.BaseAddress, path, jsonRequest, GetServerMessage(jsonResult));
        //    EntityGoneException exception = new EntityGoneException(exceptionMessage);
        //    Logger.DebugException(exceptionMessage, exception);
        //    return exception;
        //}

        //private EntityMissingPropertyException CreateEntityMissingPropertyException<RequestObjectType>(string path, string jsonResult, RequestObjectType requestBody)
        //{
        //    string jsonRequest = JsonConvert.SerializeObject(requestBody);
        //    string exceptionMessage = String.Format("API returned a BAD REQUEST response (typically malformed object or missing property). Path: {0}{1}; Request Body: {2}; Server message: {3}", _httpClient.BaseAddress, path, jsonRequest, GetServerMessage(jsonResult));
        //    EntityMissingPropertyException exception = new EntityMissingPropertyException(exceptionMessage);
        //    Logger.DebugException(exceptionMessage, exception);
        //    return exception;
        //}

        //private ConcurrencyException CreateConcurrencyException<RequestObjectType>(string path, string jsonResult, RequestObjectType requestBody)
        //{
        //    string jsonRequest = JsonConvert.SerializeObject(requestBody);
        //    string exceptionMessage = String.Format("API returned a CONCURRENCY EXCEPTION response Try submitting your request again. Path: {0}{1}; Request Body: {2}; Server message: {3}", _httpClient.BaseAddress, path, jsonRequest, GetServerMessage(jsonResult));
        //    ConcurrencyException exception = new ConcurrencyException(exceptionMessage);
        //    Logger.DebugException(exceptionMessage, exception);
        //    return exception;
        //}

        //private string GetServerMessage(string jsonResult)
        //{
        //    string serverMessage = null;

        //    try
        //    {

        //        serverMessage = JsonConvert.DeserializeObject<ApiErrorResponse>(jsonResult).Message;
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.ErrorException(string.Format("Could not deseralize json object: {0}", jsonResult), e);
        //    }

        //    if (String.IsNullOrWhiteSpace(serverMessage))
        //    {
        //        serverMessage = String.Format("API did not return an error message.");
        //    }

        //    return serverMessage;
        //}
        #endregion 
        #endregion

    }
}
