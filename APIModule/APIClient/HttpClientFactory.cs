using System.Net.Http;

namespace KP.WPF.App.APIClient
{
    public class HttpClientFactory : IHttpClientFactory
    {
        HttpClient httpClient = new HttpClient();
        public HttpClientFactory()
        {

        }
        public HttpClient GetHttpClient()
        {
            return httpClient;
        }
    }
}
