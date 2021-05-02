﻿using CustomerSite.Services;
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
        private readonly IOrderApiClient _orderApiClient;
        public CartController(ICartApiClient cartApiClient, IConfiguration configuration, IProductApiClient productApiClient,IOrderApiClient orderApiClient)
        {
            _cartApiClient = cartApiClient;
            _configuration = configuration;
            _productApiClient = productApiClient;
            _orderApiClient = orderApiClient;
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
        public async Task<IActionResult> RemoveItem(int Id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartApiClient.RemoveItem(userId, Id);
            return RedirectToAction("Index");
        }

       
        public async Task<IActionResult> clearCart()
        {
           var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           var cartVm = await _cartApiClient.GetCartByUser(userId);
            var orderVm = await _orderApiClient.GetOrderByUser(userId);
            
            var lstProduct = new List<OrderDetailVm>();
            OrderVm od = new();
            int y = orderVm.Count();
                var lstCartItem = cartVm.cartItemVms.ToList();
                if (lstCartItem.Count > 0)
                {

                    foreach (var x in lstCartItem)
                    {
                        var pVm = new OrderDetailVm()
                        {

                        };
                        pVm.ProductId = x.productVm.Id;
                        pVm.Quantity = x.Quantity;
                        pVm.UnitPrice = x.productVm.Price;
                        pVm.OrderId = y+1;

                        lstProduct.Add(pVm);
                    };
                
            }
            await _cartApiClient.clearCart(userId);
            await _orderApiClient.CreateOrder(userId, lstProduct);
            return RedirectToAction("Index", "Order");
        }
      

    }
}
