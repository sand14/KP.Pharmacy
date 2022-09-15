﻿using KP.WPF.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KP.WPF.App.APIClient.RestServices
{
    public class UserRestService : RestServiceBase, IUserRestService
    {
        public UserRestService(IHttpClientFactory httpClientFactory, IClientApplicationConfiguration configuration) : base(httpClientFactory, configuration)
        {
            client = GetClient();
        }

        HttpClient client;

        public async Task<bool> Login(string username, string password)
        {
            
                
                var encoded = Convert.ToBase64String(Encoding.GetEncoding(0).GetBytes($"{username}:{password}"));
                var json = JsonConvert.SerializeObject(encoded);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(string.Format("{0}/api/Login/", serverAddress), content);
                var result = await response.Content.ReadAsStringAsync();
                if(JsonConvert.DeserializeObject<bool>(result) && !client.DefaultRequestHeaders.Contains("Authorization"))
                    client.DefaultRequestHeaders.Add("Authorization", $"basic {encoded}");
               return JsonConvert.DeserializeObject<bool>(result);
            
        }

        public async Task<UserModel> GetUser(string username)
        {
            
                HttpRequestMessage request = await PrepareRequestMessageAsync(HttpMethod.Get, string.Format("{0}/api/Users/{1}", serverAddress, username));
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);
                return payload;
            

        }
    }
}