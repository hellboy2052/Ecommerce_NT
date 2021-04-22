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
    [Route("api/v1/[controller]")]
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
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<CartVm>>> GetAllCart()
        {
            var abc = User.Claims as ClaimsIdentity;
            var lstProduct = new List<Product>();
            var lstImage = new List<string>();
            foreach (var x in _context.Carts.Select(x => x.Products))
            {
                lstProduct = x.ToList();
            }
            var lstProductVm = new List<ProductVm>();
            foreach (var x in lstProduct)
            {

                var c = new ProductVm
                {

                    BrandId = x.BrandId,
                    CategoryId = x.CategoryId,
                    Content = x.Content,
                    Description = x.Description,
                    Id = x.Id,

                    Inventory = x.Inventory,
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                };
                foreach (var y in c.ImageLocation)
                {
                    lstImage.Add(y);
                }
                c.ImageLocation = lstImage;
                lstProductVm.Add(c);

            }
            var cartVm = await _context.Carts
                           .Select(x => new CartVm { Id = x.Id, TotalPrice = x.TotalPrice, UserId = x.UserId, productVms = lstProductVm })
                           .ToListAsync();
            return cartVm;
        }
}

        //[HttpGet("{id}")]
        ////[Authorize(Roles = "admin")]
        //public async Task<ActionResult<CartVm>> Get(int id)
        //{
        //    var cart = await _context.Carts.FindAsync(id);

        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //var lstProduct = new List<Product>();
        //var lstImage = new List<string>();
        //var lstProductVm = new List<ProductVm>();
        //foreach (var x in lstProduct)
        //{

        //    var c = new ProductVm
        //    {

        //        BrandId = x.BrandId,
        //        CategoryId = x.CategoryId,
        //        Content = x.Content,
        //        Description = x.Description,
        //        Id = x.Id,

        //        Inventory = x.Inventory,
        //        Name = x.Name,
        //        Price = x.Price,
        //        Quantity = x.Quantity,
        //    };
        //    foreach (var y in c.ImageLocation)
        //    {
        //        lstImage.Add(y);
        //    }
        //    c.ImageLocation = lstImage;
            

        //}

        //return c;
        //}
        
        //[HttpGet("{userId}")]
        ////[Authorize(Roles = "admin")]
        //public async Task<ActionResult<CartVm>> Get(string userId)
        //{
        //    var cart =await _context.Carts.FirstOrDefaultAsync(x=>x.UserId==userId);

        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }
        //    var cartVm = new CartVm
        //    {
        //        Id = cart.Id,
        //        TotalPrice = cart.TotalPrice,
        //        UserId = cart.UserId

        //    };
        //    var pVm = new CartItemVm();
        //    List<CartItemVm> lstProducts = new();
        //    foreach (CartItem p in cart.CartItems)
        //    {

        //        pVm.Id = p.Id;

        //        pVm.Inventory = p.Inventory;
        //        pVm.Name = p.Name;
        //        pVm.Price = p.Price;

        //        pVm.Quantity = p.Quantity;
        //        pVm.ImageFirst = p.ImageFirst;
        //        lstProducts.Add(pVm);
        //    }
        //    cartVm.cartItemVms = lstProducts;
        //    return cartVm;
        //}
       
        //[HttpPost()]
        //////[Authorize(Roles = "admin")]
        //[AllowAnonymous]
        //public async Task<ActionResult> Post(CartVm cartVm)
        //{
        //    List<CartItem> lstProducts = new();
        //    foreach (var p in cartVm.cartItemVms.ToList())
        //    {
        //        var pVm = new CartItem();

        //        pVm.Id = p.Id;
        //        pVm.Inventory = p.Inventory;
        //        pVm.Name = p.Name;
        //        pVm.Price = p.Price;
 
        //        pVm.Quantity = p.Quantity;
        //        lstProducts.Add(pVm);
        //    }
        //    var cart = new Cart
        //    {
        //        CartItems = lstProducts,
        //        Id=cartVm.Id,
        //        TotalPrice=cartVm.TotalPrice,
        //        UserId=cartVm.UserId
                
        //    };

        //    _context.Carts.Add(cart);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("Get", new { id = cart.Id }, new CartVm { Id = cart.Id, UserId = cart.UserId,TotalPrice=cart.TotalPrice,
        //    cartItemVms=cartVm.cartItemVms});


        //}
        
        //[HttpPut("{userId}/{productId}")]
        ////[Authorize(Roles = "admin")]
        //public async Task<IActionResult> RemoveItem(string userId, int productId)
        //{
        //    var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId);
        //    foreach (CartItem p in cart.CartItems)
        //    {
        //        if (p.Id == productId)
        //        {
        //            p.Quantity--;
        //            if (p.Quantity == 0)
        //            {
        //                cart.CartItems.Remove(p);
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
        //[HttpPut("{userId1}/{productId1}")]
        ////[Authorize(Roles = "admin")]
        //public async Task<IActionResult> AddItem(string userId1, int productId1)
        //{
        //    var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId1);
        //    foreach (CartItem p in cart.CartItems)
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
    //}
}
