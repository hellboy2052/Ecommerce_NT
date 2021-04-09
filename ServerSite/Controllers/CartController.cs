﻿using Microsoft.AspNetCore.Mvc;
using ServerSite.Data;
using ServerSite.Models;
using SharedVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Bearer")]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<CartVm>> Post(CartVm cartVm)
        {
            var cart = new Cart
            {

                Id = cartVm.Id,
                Total = cartVm.Total,
                UserId = cartVm.UserId,
                cartProducts = new List<CartProduct>()
                
            };
            
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCR", new { id = cart.Id }, new Cart { Id = cart.Id,
                cartProducts=cart.cartProducts,Total=cart.Total,UserId=cart.UserId });
        }
        //[HttpPut("{id}")]
        ////[Authorize(Roles = "admin")]
        //public async Task<IActionResult> Put(int id, CartVm cartVm, List<CartProduct> cartProducts)
        //{
        //    var cart = await _context.Carts.FindAsync(id);

        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    cart.UserId = cartVm.UserId;
        //    cart.Total = cartVm.Total;
        //    cart.cartProducts = cartProducts;
           
        //    await _context.SaveChangesAsync();

        //    return Accepted();
        //}
        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveItem(int id, CartProduct cartProduct)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            
                cart.cartProducts.Remove(cartProduct);
            

            await _context.SaveChangesAsync();

            return Accepted();
        }
        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> AddItem(int id, CartProduct cartProduct)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

           
                cart.cartProducts.Add(cartProduct);
            

            await _context.SaveChangesAsync();

            return Accepted();
        }
    }
}
