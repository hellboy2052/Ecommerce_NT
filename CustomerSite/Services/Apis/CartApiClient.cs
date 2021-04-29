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
using System.Security.Claims;
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
        public async Task<CartVm> CreateCart(CartVm cartVm)
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

        public async Task<CartVm> GetCartByUser(string userId)
        {
            var client = _httpClientFactory.CreateClient();
            var response1 = await client.GetAsync(_configuration["BackendUrl:Default"] + "/api/Cart/getCartByUser/"+userId);
            if (response1.StatusCode==System.Net.HttpStatusCode.NotFound)
            {
                CartVm cartVm = new()
                {
                    UserId = userId,
                    cartItemVms=new List<CartItemVm>(),
                    TotalPrice=0
                };
               

                //Send json with body
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(cartVm),
                    Encoding.UTF8, "application/json");
                await client.PostAsync(_configuration["BackendUrl:Default"] + "/api/Cart", httpContent);
            }
            var response = await client.GetAsync(_configuration["BackendUrl:Default"] + "/api/Cart/getCartByUser/" + userId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CartVm>();
        }
        public async Task<CartVm> AddCartItem(string userId,int productId,int quantity)
        {
            await GetCartByUser(userId);
            var client = _httpClientFactory.CreateClient();
            var response1 = await client.GetAsync(_configuration["BackendUrl:Default"] + "/api/Product/" + productId);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(response1),
               Encoding.UTF8, "application/json");
            var response = await client.PutAsync(_configuration["BackendUrl:Default"] + "/api/Cart/addCartItem/" + userId + "/" + productId+"/"+ quantity, httpContent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CartVm>();
        }
    }
}
