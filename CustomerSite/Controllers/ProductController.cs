using CustomerSite.Services;
using CustomerSite.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedVm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        public ProductController(IProductApiClient productApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
        }
        //public async Task<IActionResult> IndexAsync()
        //{
        //    var products = await _productApiClient.Get();


        //    return View(products);
        //}
        public async Task<IActionResult> GetProductByCategory(int idCategory)
        {
            var products = await _productApiClient.Get1(idCategory);
            foreach (var x in products)
            {
                for (int i = 0; i < x.ImageLocation.Count; i++)
                {
                    string setUrl = _configuration["BackendUrl:Default"] + x.ImageLocation[i];
                    x.ImageLocation[i] = setUrl;
                }
            }
            return View(products);
            
        }
        public async Task<IActionResult> Detail(int id)
        {

            var product = await _productApiClient.Get(id);

            for (int i = 0; i < product.ImageLocation.Count; i++)
            {
                string setUrl = _configuration["BackendUrl:Default"] + product.ImageLocation[i];
                product.ImageLocation[i] = setUrl;
            }

            return View(product);
        }
        //[HttpPost("{id}")]
        public async Task<IActionResult> AddsSession(int id,int quantity)
        {
            List<ProductVm> ListProduct = HttpContext.Session.Get<List<ProductVm>>("SessionCart");

            if (ListProduct == null)
            {
                ListProduct = new List<ProductVm>();
            }

            var product = await _productApiClient.Get(id);
            for (int i = 0; i < product.ImageLocation.Count; i++)
            {
                string setUrl = _configuration["BackendUrl:Default"] + product.ImageLocation[i];
                product.ImageLocation[i] = setUrl;
            }

            ProductVm x = new ProductVm();
            x.ImageLocation = product.ImageLocation;
            x.Name = product.Name;
            x.Quantity = quantity;
            x.Price = product.Price;
            ListProduct.Add(x);
            HttpContext.Session.Set("SessionCart", ListProduct);

            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }



    }
}
