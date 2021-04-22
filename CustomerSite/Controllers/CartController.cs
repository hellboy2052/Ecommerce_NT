using CustomerSite.Services;
using CustomerSite.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedVm;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;

namespace CustomerSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartApiClient _cartApiClient;
        private readonly IConfiguration _configuration;
        public CartController(ICartApiClient cartApiClient, IConfiguration configuration)
        {
            _cartApiClient = cartApiClient;
            _configuration = configuration;
        }

        public IActionResult Index(string userId)
        {
            List<ProductVm> ListProduct = new List<ProductVm>();
            CartVm cartVm = new CartVm();
            if (!User.Identity.IsAuthenticated)
            {
                ListProduct = HttpContext.Session.Get<List<ProductVm>>("SessionCart");
            }
            else
            {
                cartVm = _cartApiClient.Get();
                var lstProduct = cartVm.productVms.ToList();
                foreach(var x in lstProduct)
                {
                    var pd = new ProductVm
                    {
                        BrandId = x.BrandId,
                        CategoryId=x.CategoryId,
                        Content=x.Content,
                        Description=x.Description,
                        Id=x.Id,
                        ImageLocation=x.ImageLocation,
                        Inventory=x.Inventory,
                        Name=x.Name,
                        Price=x.Price,
                        Quantity=x.Quantity
                    };
                    ListProduct.Add(pd);
                }
                

            }
            if (ListProduct == null)
            {
                return NotFound();
            }
            return View(ListProduct);
        }
        //[HttpPost("{id}")]
        //public async Task<IActionResult> AddsCart( int id,CartVm cartVm)
        //{
        //    cartVm.UserId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    cartVm.ProductId = id;
        //    cartVm.TotalPrice = 0;
        //    var cart = await _cartApiClient.CreateCart(cartVm);

        //    string referer = Request.Headers["Referer"].ToString();
        //    return RedirectToAction(referer);
        //}

    }
}
