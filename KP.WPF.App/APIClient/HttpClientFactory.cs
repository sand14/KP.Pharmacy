using System.Net.Http;

namespace KP.WPF.App.APIClient
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient GetHttpClient()
        {
            return new HttpClient();
        }
    }
}
