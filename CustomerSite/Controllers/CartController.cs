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

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<ProductVm> ListProduct = new List<ProductVm>();
            CartVm cartVm = new CartVm
            {
                UserId = userId
            };
            if (!User.Identity.IsAuthenticated)
            {
                ListProduct = HttpContext.Session.Get<List<ProductVm>>("SessionCart");
            }
            else
            {
               
                    cartVm = await _cartApiClient.GetCartByUser(userId);
                    var lstCartItem = cartVm.cartItemVms.ToList();
                    var lstProduct = new List<CartItemVm>();
                    if (lstCartItem.Count > 0) { 
                    
                    foreach (var x in lstCartItem)
                    {
                        var pVm = new CartItemVm();
                        
                         

                        pVm.productVm.BrandId = x.productVm.BrandId;
                        pVm.productVm.CategoryId = x.productVm.CategoryId;
                        pVm.productVm.Content = x.productVm.Content;
                        pVm.productVm.Description = x.productVm.Description;
                        pVm.productVm.Id = x.Id;

                        pVm.productVm.Inventory = x.productVm.Inventory;
                        pVm.productVm.Name = x.productVm.Name;
                        pVm.productVm.Price = x.productVm.Price;
                        pVm.productVm.Quantity = x.productVm.Quantity;
                        lstProduct.Add(pVm);
                    };
                    }
                    return View(lstProduct);
                    RedirectToAction("Index");
                

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
