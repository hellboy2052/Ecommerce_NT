using CustomerSite.Services;
using CustomerSite.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedVm;
using System.Collections.Generic;

namespace CustomerSite.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;
        public CartController(IProductApiClient productApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
        }
        //add Session
        //public IActionResult Index()
        //{
        //    List<CartProductVm> ListProduct = HttpContext.Session.Get<List<CartProductVm>>("SessionCart");
        //    if (ListProduct == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ListProduct);
        //}


    }
}
