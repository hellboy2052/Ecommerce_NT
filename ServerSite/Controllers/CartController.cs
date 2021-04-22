using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerSite.Data;
using ServerSite.Models;
using SharedVm;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ServerSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CartVm>>> GetAllCart()
        {

            var lstCartItem = new List<CartItem>();
            var lstCartItemVm = new List<CartItemVm>();
            var lstImage = new List<string>();
            foreach (var x in _context.Carts.Select(x => x.CartItems))
            {
                lstCartItem = x.ToList();
            }
            var cartItemVm = new CartItemVm();
            var lstProductVm = new List<ProductVm>();
            foreach (var x in lstCartItem)
            {

                var c = new ProductVm
                {

                    BrandId = x.Product.BrandId,
                    CategoryId = x.Product.CategoryId,
                    Content = x.Product.Content,
                    Description = x.Product.Description,
                    Id = x.Id,

                    Inventory = x.Product.Inventory,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    Quantity = x.Product.Quantity,
                };
                foreach (var y in c.ImageLocation)
                {
                    lstImage.Add(y);
                }
                c.ImageLocation = lstImage;
                cartItemVm.productVm = c;
                lstCartItemVm.Add(cartItemVm);
            }
            
            var cartVm = await _context.Carts
                           .Select(x => new CartVm { Id = x.Id, TotalPrice = x.TotalPrice, UserId = x.UserId, cartItemVms = lstCartItemVm })
                           .ToListAsync();
            return cartVm;
        }


        [HttpGet("{id}")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<ActionResult<CartVm>> GetCartById(int id)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            var lstProduct = cart.CartItems.ToList();
            var lstImage = new List<string>();
            var cartItemVms = new List<CartItemVm>();
            var cartItemVm = new CartItemVm();
            foreach (var x in lstProduct)
            {

                var c = new ProductVm
                {

                    BrandId = x.Product.BrandId,
                    CategoryId = x.Product.CategoryId,
                    Content = x.Product.Content,
                    Description = x.Product.Description,
                    Id = x.Id,

                    Inventory = x.Product.Inventory,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    Quantity = x.Product.Quantity,
                };
                foreach (var y in c.ImageLocation)
                {
                    lstImage.Add(y);
                }

                c.ImageLocation = lstImage;
                cartItemVm.productVm = c;
                cartItemVms.Add(cartItemVm);

            }

            var cartVm = new CartVm { Id = cart.Id, TotalPrice = cart.TotalPrice, UserId = cart.UserId, cartItemVms = cartItemVms };
            return cartVm;
        }

        [HttpGet("getCartByUser/{userId}")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<ActionResult<CartVm>> GetCartByUser(string userId)
        {
           
            var cart = await _context.Carts.Include(x=>x.CartItems).FirstOrDefaultAsync(x => x.UserId == userId);

            if (cart == null)
            {
                return NotFound();
            }

            var lstProduct = cart.CartItems.ToList();
            var lstImage = new List<string>();
            var cartItemVms = new List<CartItemVm>();
            var cartItemVm = new CartItemVm();
            foreach (var x in lstProduct)
            {

                var c = new ProductVm
                {

                    BrandId = x.Product.BrandId,
                    CategoryId = x.Product.CategoryId,
                    Content = x.Product.Content,
                    Description = x.Product.Description,
                    Id = x.Id,

                    Inventory = x.Product.Inventory,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    Quantity = x.Product.Quantity,
                };
                foreach (var y in c.ImageLocation)
                {
                    lstImage.Add(y);
                }
                
                c.ImageLocation = lstImage;
                cartItemVm.productVm = c;
                cartItemVms.Add(cartItemVm);

            }

            var cartVm = new CartVm { Id = cart.Id, TotalPrice = cart.TotalPrice, UserId = cart.UserId, cartItemVms = cartItemVms };
            return cartVm;
        }

        [HttpPost()]
        ////[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<ActionResult<CartVm>> CreateCart(CartVm cartVm)
        {
            List<CartItem> lstProducts = new();
            foreach (var x in cartVm.cartItemVms.ToList())
            {
                var pVm = new CartItem();

                pVm.Product.BrandId = x.productVm.BrandId;
                pVm.Product.CategoryId = x.productVm.CategoryId;
                pVm.Product.Content = x.productVm.Content;
                    pVm.Product.Description = x.productVm.Description;
                pVm.Product.Id = x.Id;

                    pVm.Product.Inventory = x.productVm.Inventory;
                pVm.Product.Name = x.productVm.Name;
                pVm.Product.Price = x.productVm.Price;
                pVm.Product.Quantity = x.productVm.Quantity;
                lstProducts.Add(pVm);
            }
            var cart = new Cart
            {
                CartItems = lstProducts,
                Id = cartVm.Id,
                TotalPrice = cartVm.TotalPrice,
                UserId = cartVm.UserId

            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = cart.Id }, new CartVm
            {
                Id = cart.Id,
                UserId = cart.UserId,
                TotalPrice = cart.TotalPrice,
                cartItemVms = cartVm.cartItemVms
            });


        }

        //[HttpPut("removeItem/{userId}/{productId}")]
        ////[Authorize(Roles = "admin")]
        //[AllowAnonymous]
        //public async Task<IActionResult> RemoveItem(string userId, int productId)
        //{
        //    var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId);
        //    foreach (Product p in cart.Products)
        //    {
        //        if (p.Id == productId)
        //        {
        //            p.Quantity--;
        //            if (p.Quantity == 0)
        //            {
        //                cart.Products.Remove(p);
        //            }
        //        }
        //    }
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
        //[HttpPut("addItem/{userId1}/{productId1}")]
        ////[Authorize(Roles = "admin")]
        //[AllowAnonymous]
        //public async Task<IActionResult> AddItem(string userId1, int productId1)
        //{
        //    var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId1);
        //    foreach (Product p in cart.Products)
        //    {
        //        if (p.Id == productId1)
        //        {
        //            p.Quantity++;
        //        }
        //    }
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }

}