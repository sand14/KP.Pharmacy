using System.Net.Http;

namespace KP.WPF.App.APIClient
{
    public interface IHttpClientFactory
    {
        HttpClient GetHttpClient();
    }
}
