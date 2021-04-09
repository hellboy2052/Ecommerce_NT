using CustomerSite.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CustomerSite.Controllers
{
    public class ProductController:Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        public ProductController(IProductApiClient productApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var products = await _productApiClient.Get();


            return View(products);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productApiClient.GetId(id);

            for (int i = 0; i < product.ImageLocation.Count; i++)
            {
                string setUrl = _configuration["BackendUrl:Default"] + product.ImageLocation[i];
                product.ImageLocation[i] = setUrl;
            }

            return View(product);
        }


    }
}
