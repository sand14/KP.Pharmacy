using KP.WPF.App.APIClient;
using KP.WPF.App.APIClient.RestServices;
using KP.WPF.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;



namespace KP.WPF.APIModule.APIClient.RestServices
{
    public class ProductRestService : RestServiceBase
    {
        HttpClient client;
        public ProductRestService(IClientApplicationConfiguration configuration) : base(configuration)
        {
            client = GetClient();
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            HttpRequestMessage request = await PrepareRequestMessageAsync(HttpMethod.Get, string.Format("{0}/api/Products", serverAddress));
            var response = await client.SendAsync(request);
            var payload = DeserializeObject<List<ProductModel>>(await response.Content.ReadAsStringAsync());
            return payload;
        }

        public async Task<ProductModel> CreateProductAsync(ProductModel product)
        {

            var data = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{serverAddress}/api/Products", data);
            var payload = DeserializeObject<ProductModel>(await response.Content.ReadAsStringAsync());
            return payload;
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            var request = await PrepareRequestMessageAsync(HttpMethod.Delete, string.Format("{0}/api/Products/{1}", serverAddress, productId));
            var response = await client.SendAsync(request);
            await Task.CompletedTask;
        }

        public async Task<ProductModel> UpdateProductAsync(Guid productId, ProductModel product)
        {
            var request = await PrepareRequestMessageAsync(HttpMethod.Put, string.Format("{0}/api/Products/{1}", serverAddress, productId));
            var jsonPayload = SerializeObject(product);
            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            var payload = DeserializeObject<ProductModel>(response.Content.ReadAsStringAsync().Result);
            return payload;
        }
    }


}
