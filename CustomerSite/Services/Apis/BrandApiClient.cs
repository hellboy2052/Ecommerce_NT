
using CustomerSite.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using SharedVm;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerSite.Services.Apis
{
    public class BrandApiClient : IBrandApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public BrandApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IList<BrandVm>> Get()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["BackendUrl:Default"] + "/api/Brand");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<BrandVm>>();
        }
    }
}
