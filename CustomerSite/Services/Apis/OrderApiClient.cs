using CustomerSite.Services.Interfaces;
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
using Microsoft.AspNetCore.Http;

namespace CustomerSite.Services.Apis
{
    public class OrderApiClient:IOrderApiClient
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;


        public OrderApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IList<OrderVm>> GetOrderByUser(string userId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["BackendUrl:Default"] + "/api/Order/getOrderByUser/" + userId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IList<OrderVm>>();
        }
        public async Task<OrderVm> CreateOrder(string userId, List<OrderDetailVm> orderDetailVm1)
        {

            var client = _httpClientFactory.CreateClient();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(orderDetailVm1),
               Encoding.UTF8, "application/json");
           
            var response = await client.PostAsync(_configuration["BackendUrl:Default"] + "/api/Order/" + userId , httpContent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<OrderVm>();
        }
    }
}
