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
using CustomerSite.Services;


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
        [HttpPost("{id}")]
        public async Task<IActionResult> AddsSession(int id)
        {
            List<CartProductVm> ListProduct = HttpContext.Session.Get<List<CartProductVm>>("SessionCart");

            if (ListProduct == null)
            {
                ListProduct = new List<CartProductVm>();
            }

            var product = await _productApiClient.GetId(id);
            for (int i = 0; i < product.ImageLocation.Count; i++)
            {
                string setUrl = _configuration["BackendUrl:Default"] + product.ImageLocation[i];
                product.ImageLocation[i] = setUrl;
            }

            CartProductVm x = new CartProductVm();
            x.ImageLocation = product.ImageLocation;
            x.ProductID = product.Id;
            //x.Quantity = quantity;
            x.Price = product.Price;
            x.ProductName = product.Name;
            ListProduct.Add(x);
            HttpContext.Session.Set("SessionCart", ListProduct);

            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }

        

    }
}
