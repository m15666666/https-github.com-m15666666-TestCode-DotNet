using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Moons.Common20.WebApi
{
    /// <summary>
    /// 使用System.Net.Http.HttpClient访问webapi代理类的基类
    /// </summary>
    public abstract class WebApiClientBase
    {
        private System.Net.Http.HttpClient _httpClient;
        private Lazy<JsonSerializerOptions> _settings;

        public WebApiClientBase(string baseUrl, System.Net.Http.HttpClient httpClient)
        {
            BaseUrl = baseUrl;
            _httpClient = httpClient;
            _settings = new Lazy<JsonSerializerOptions>(() =>
            {
                var settings = new JsonSerializerOptions() { 
                    PropertyNameCaseInsensitive = true,
                };
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }

        public string BaseUrl { get; set; }

        protected JsonSerializerOptions JsonSerializerSettings { get { return _settings.Value; } }

        protected virtual void UpdateJsonSerializerSettings(JsonSerializerOptions settings)
        {
        }

        protected virtual void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
        }

        protected virtual void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder)
        {
        }

        protected virtual void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response)
        {
        }

        public async System.Threading.Tasks.Task<TOutput> PostAsync<TInput, TOutput>(string url, TInput input, System.Threading.CancellationToken cancellationToken) where TOutput : class
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append(url);

            var client_ = _httpClient;
            using (var request_ = new System.Net.Http.HttpRequestMessage())
            {
                var stringContent = JsonSerializer.Serialize(input, _settings.Value);
                var content_ = new System.Net.Http.StringContent(stringContent);
                content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                request_.Content = content_;
                request_.Method = new System.Net.Http.HttpMethod("POST");
                request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                PrepareRequest(client_, request_, urlBuilder_);
                var url_ = urlBuilder_.ToString();

                TraceUtils.Debug($"request: {url_}.");
                TraceUtils.Debug($"body: {stringContent}.");

                request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                PrepareRequest(client_, request_, url_);

                var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                try
                {
                    var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                    if (response_.Content != null && response_.Content.Headers != null)
                    {
                        foreach (var item_ in response_.Content.Headers)
                            headers_[item_.Key] = item_.Value;
                    }

                    ProcessResponse(client_, response_);

                    var status_ = ((int)response_.StatusCode).ToString();

                    TraceUtils.Debug($"status: {status_}.");

                    if (status_ == "200")
                    {
                        var objectResponse_ = await ReadObjectResponseAsync<TOutput>(response_, headers_).ConfigureAwait(false);
                        return objectResponse_.Object;
                    }
                    else if (status_ != "200" && status_ != "204")
                    {
                        var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);

                        TraceUtils.Debug($"responseData_:{responseData_}.");

                        throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                    }

                    return default;
                }
                finally
                {
                    if (response_ != null) response_.Dispose();
                }
            }
        }

        /// <summary>
        /// http流读取的结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                Object = responseObject;
                Text = responseText;
            }

            public T Object { get; }

            public string Text { get; }
        }

        /// <summary>
        /// 是否直接读出字符串再解析json还是从流直接解析json对象
        /// </summary>
        public bool ReadResponseAsString { get; set; }

        /// <summary>
        /// 从http流读取json对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        protected virtual async System.Threading.Tasks.Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, IReadOnlyDictionary<string, IEnumerable<string>> headers) where T : class
        {
            if (response == null || response.Content == null) return new ObjectResponseResult<T>(default, string.Empty);

            if (typeof(T) == typeof(string))
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return new ObjectResponseResult<T>(responseText as T, responseText);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var typedBody = JsonSerializer.Deserialize<T>(responseText, JsonSerializerSettings);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (Exception exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
                }
            }

            try
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    var typedBody = await JsonSerializer.DeserializeAsync<T>(responseStream, JsonSerializerSettings);
                    return new ObjectResponseResult<T>(typedBody, string.Empty);
                }
            }
            catch (Exception exception)
            {
                var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
            }
        }
    }

    /// <summary>
    /// 解析json抛出的异常
    /// </summary>
    public partial class ApiException : Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; private set; }

        public ApiException(string message, int statusCode, string response, IReadOnlyDictionary<string, IEnumerable<string>> headers, Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + response.Substring(0, response.Length >= 512 ? 512 : response.Length), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString() => string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
    }
}