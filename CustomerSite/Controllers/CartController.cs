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
        private readonly IProductApiClient _productApiClient;
        public CartController(ICartApiClient cartApiClient, IConfiguration configuration, IProductApiClient productApiClient)
        {
            _cartApiClient = cartApiClient;
            _configuration = configuration;
            _productApiClient = productApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<CartItemVm> ListProduct = new();
            CartVm cartVm = new()
            {
                UserId = userId
            };
            if (!User.Identity.IsAuthenticated)
            {
                ListProduct = HttpContext.Session.Get<List<CartItemVm>>("SessionCart");
            }
            else
            {
               
                    cartVm = await _cartApiClient.GetCartByUser(userId);
                    var lstCartItem = cartVm.cartItemVms.ToList();
                    var lstProduct = new List<CartItemVm>();
                    if (lstCartItem.Count > 0) { 
                    
                    foreach (var x in lstCartItem)
                    {
                        var pVm = new CartItemVm()
                        {
                            productVm = new ProductVm(),
                        };
                            
                        
                         

                       
                        pVm.productVm.CategoryId = x.productVm.CategoryId;
                        
                        pVm.productVm.Description = x.productVm.Description;
                        pVm.productVm.Id = x.Id;
                        pVm.productVm.ImageLocation = x.productVm.ImageLocation;
                        pVm.productVm.Inventory = x.productVm.Inventory;
                        pVm.productVm.Name = x.productVm.Name;
                        pVm.productVm.Price = x.productVm.Price;
                        pVm.Quantity = x.Quantity;
                       

                        pVm.productVm.AverageStar = x.productVm.AverageStar;
                        lstProduct.Add(pVm);
                    };
                    }
                    return View(lstProduct);
                   
                

            }
            if (ListProduct == null)
            {
                return NotFound();
            }
            return View(ListProduct);
        }

        public async Task<IActionResult> AddCartItem(string userId,int productId,int quantity)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartVm = await _cartApiClient.AddCartItem(userId, productId, quantity);
            return Redirect("Index");
        }
        public async Task<IActionResult> AddsSession(int id, int quantity)
        {
            //List<ProductVm> ListProduct = HttpContext.Session.Get<List<ProductVm>>("SessionCart");
            List<CartItemVm> ListCartItemVm = HttpContext.Session.Get<List<CartItemVm>>("SessionCart");

            if (ListCartItemVm == null)
            {
                ListCartItemVm = new List<CartItemVm>();
            }

            var product = await _productApiClient.GetProductById(id);
            for (int i = 0; i < product.ImageLocation.Count; i++)
            {
                string setUrl = _configuration["BackendUrl:Default"] + product.ImageLocation[i];
                product.ImageLocation[i] = setUrl;
            }

            ProductVm x = new();
            x.ImageLocation = product.ImageLocation;
            x.Name = product.Name;

            x.Price = product.Price;
            CartItemVm cartItemVm = new();
            cartItemVm.productVm = x;
            cartItemVm.Quantity = quantity;
            ListCartItemVm.Add(cartItemVm);

            HttpContext.Session.Set("SessionCart", ListCartItemVm);

            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
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
