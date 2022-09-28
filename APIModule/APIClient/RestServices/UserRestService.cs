using KP.WPF.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Auth;
using Prism.Events;

namespace KP.WPF.App.APIClient.RestServices
{
    public class UserRestService : RestServiceBase
    {
        private IEventAggregator ea;
        public UserRestService(IClientApplicationConfiguration configuration, IEventAggregator ea) : base(configuration)
        {
            client = GetClient();
            this.ea = ea;
        }

        HttpClient client;

        static Guid loggedGuid;

        public async Task<UserModel> Login(string username, string password)
        {


            var encoded = Convert.ToBase64String(Encoding.GetEncoding(0).GetBytes($"{username}:{password}"));
            var json = JsonConvert.SerializeObject(encoded);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(string.Format("{0}/api/Login/", serverAddress), content);
            var result = await response.Content.ReadAsStringAsync();
            if ((JsonConvert.DeserializeObject<UserModel>(result) != null)  && (!client.DefaultRequestHeaders.Contains("Authorization")))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"basic {encoded}");
                loggedGuid = JsonConvert.DeserializeObject<UserModel>(result).UserId;
            }
            return JsonConvert.DeserializeObject<UserModel>(result);


        }

        public void Logout()
        {
            client.DefaultRequestHeaders.Clear();
        }

        public async Task<UserModel> GetUser(string username)
        {

            HttpRequestMessage request = await PrepareRequestMessageAsync(HttpMethod.Get, string.Format("{0}/api/Users/{1}", serverAddress, username));
            var response = await client.SendAsync(request);
            var payload = DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);
            return payload;
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            HttpRequestMessage request = await PrepareRequestMessageAsync(HttpMethod.Get, string.Format("{0}/api/Users", serverAddress));
            var response = await client.SendAsync(request);
            var payload = DeserializeObject<List<UserModel>>(await response.Content.ReadAsStringAsync());
            return payload;
        }

        public async Task<UserModel> CreateUserAsync(UserModel product)
        {

            var data = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{serverAddress}/api/Users", data);
            var payload = DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync());
            return payload;
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            if (loggedGuid == userId)
            {
                MessageBox.Show("You cannot delete you own account");
                return;
            }
            var request = await PrepareRequestMessageAsync(HttpMethod.Delete, string.Format("{0}/api/Users/{1}", serverAddress, userId));
            var response = await client.SendAsync(request);
            await Task.CompletedTask;
        }

        public async Task<UserModel> UpdateUserAsync(UserModel user)
        {
            var request = await PrepareRequestMessageAsync(HttpMethod.Put, string.Format("{0}/api/Users/", serverAddress));
            var jsonPayload = SerializeObject(user);
            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            var payload = DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);
            if (loggedGuid == user.UserId)
            {
                var oldheader = client.DefaultRequestHeaders.Authorization.ToString();
                var oldauthorization = oldheader.Substring(6);
                var bytes = Convert.FromBase64String(oldauthorization);
                string credentials = Encoding.UTF8.GetString(bytes);
                string[] array = credentials.Split(":");
                string oldpassword = array[1];

                if (user.Password.StartsWith("$2a$11$"))
                {
                    var encoded =
                        Convert.ToBase64String(Encoding.GetEncoding(0).GetBytes($"{user.Username}:{oldpassword}"));
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", $"basic {encoded}");
                }
                else
                {
                    var encoded =
                        Convert.ToBase64String(Encoding.GetEncoding(0).GetBytes($"{user.Username}:{user.Password}"));
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", $"basic {encoded}");
                }

                if (user.IsAdmin == false)
                {
                    ea.GetEvent<MessageSentEvent>().Publish("AdminDisabled");
                }
            }
            return payload;
        }
    }
}
