﻿using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KP.WPF.App.APIClient.RestServices
{
    public class RestServiceBase
    {
        protected string serverAddress;
        private readonly IHttpClientFactory httpClientFactory;
        private const string contentType = "application/json";

        static RestServiceBase()
        {
            // accept all ssl certificates.
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        public HttpClient GetClient()
        {
            return httpClientFactory.GetHttpClient();
        }

        public T DeserializeObject<T>(string jsonstring)
        {
            return JsonConvertWrapper.DeserializeObject<T>(jsonstring);
        }

        public T DeserializeObject<T>(string jsonstring, JsonConverter[] converters)
        {
            return JsonConvertWrapper.DeserializeObject<T>(jsonstring, converters);
        }

        public string SerializeObject(object value)
        {
            return JsonConvertWrapper.SerializeObject(value);
        }

  

        protected async Task<HttpRequestMessage> PrepareRequestMessageAsync(HttpMethod method, string requestUri)
        {
            var request = new HttpRequestMessage(method, requestUri.Replace(@"//api/", @"/api/"));
            //var encoded = Convert.ToBase64String(Encoding.GetEncoding(0).GetBytes($"{username}:{password}"));
            //request.Headers.Add("Authorization", $"basic {encoded}");
            return request;
        }

        protected HttpRequestMessage PrepareRequestMessageNoAuth(HttpMethod method, string requestUri)
        {
            var request = new HttpRequestMessage(method, requestUri);
            return request;
        }

        public RestServiceBase(IHttpClientFactory httpClientFactory, IClientApplicationConfiguration configuration)

        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));


            this.serverAddress = configuration.ServerAddress;

            if (httpClientFactory == null)
                throw new ArgumentNullException("httpClientFactory");
            this.httpClientFactory = httpClientFactory;

        }

        protected string ToQueryString(NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         where !string.IsNullOrEmpty(value.Trim())
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }
    }
}
