using CustomerSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerSite.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandApiClient _brandApiClient;
        private readonly IConfiguration _configuration;

        public BrandController(IBrandApiClient brandApiClient, IConfiguration configuration)
        {
            _brandApiClient = brandApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var brands = await _brandApiClient.Get();

            
            return View(brands);
        }

        //public async Task<IActionResult> GetProductByCategory(int idCate)
        //{
        //    var products = await _productApiClient.GetProductByCategory(idCate);

        //    foreach (var x in products)
        //    {
        //        for (int i = 0; i < x.ImageLocation.Count; i++)
        //        {
        //            string setUrl = _configuration["BackendUrl:Default"] + x.ImageLocation[i];
        //            x.ImageLocation[i] = setUrl;
        //        }
        //    }
        //    return View(products);
        //}

        //public async Task<IActionResult> Detail(int id)
        //{
        //    var product = await _productApiClient.GetProduct(id);

        //    for (int i = 0; i < product.ImageLocation.Count; i++)
        //    {
        //        string setUrl = _configuration["BackendUrl:Default"] + product.ImageLocation[i];
        //        product.ImageLocation[i] = setUrl;
        //    }

        //    return View(product);
        //}
    }
}
