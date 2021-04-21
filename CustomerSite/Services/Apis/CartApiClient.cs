﻿using CustomerSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SharedVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSite.Services.Apis
{
    public class CartApiClient : ICartApiClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public CartApiClient( IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CartVm> Post(CartVm cartVm)
        {
            //Send access token 
            var client = _httpClientFactory.CreateClient();

            //Send json with body
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(cartVm),
                Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_configuration["BackendUrl:Default"] + "/api/Cart",httpContent);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CartVm>();
     
        }
        public async Task<CartVm> Get(string userId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["BackendUrl:Default"] + "/api/Cart/"+userId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CartVm>();
        }
    }
}
