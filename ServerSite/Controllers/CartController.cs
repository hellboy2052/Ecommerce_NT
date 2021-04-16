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
    //[Authorize("Bearer")]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<CartVm>>> GetAllCart()
        {
            return await _context.Carts
                .Select(x => new CartVm { Id = x.Id, TotalPrice = x.TotalPrice, UserId = x.UserId })
                .ToListAsync();

        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<CartVm>> GetCartById(int id)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            var cartVm = new CartVm
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                UserId = cart.UserId
            };
            var pVm = new ProductVm();
            List<ProductVm> productVms = new();
            List<ProductVm> lstProducts = productVms;
            foreach (Product p in cart.Product)
            {
                pVm.BrandId = p.BrandId;
                pVm.CategoryId = p.CategoryId;
                pVm.Description = p.Description;
                pVm.Id = p.Id;
                pVm.ImageLocation = new List<string>();
                pVm.Inventory = p.Inventory;
                pVm.Name = p.Name;
                pVm.Price = p.Price;
                pVm.Content = p.Content;
                pVm.Quantity = p.Quantity;
                lstProducts.Add(pVm);
            }
            cartVm.ProductVms = lstProducts;

            return cartVm;
        }
        
        [HttpGet("{userId}")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<CartVm>> GetCartByUserId(string userId)
        {
            var cart =await _context.Carts.FirstOrDefaultAsync(x=>x.UserId==userId);

            if (cart == null)
            {
                return NotFound();
            }
            var cartVm = new CartVm
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                UserId = cart.UserId

            };
            var pVm = new ProductVm();
            List<ProductVm> lstProducts = new();
            foreach (Product p in cart.Product)
            {
                pVm.BrandId = p.BrandId;
                pVm.CategoryId = p.CategoryId;
                pVm.Description = p.Description;
                pVm.Id = p.Id;
                pVm.ImageLocation = new List<string>();
                pVm.Inventory = p.Inventory;
                pVm.Name = p.Name;
                pVm.Price = p.Price;
                pVm.Content = p.Content;
                pVm.Quantity = p.Quantity;
                lstProducts.Add(pVm);
            }
            cartVm.ProductVms = lstProducts;
            return cartVm;
        }
        async Task<bool> checkCartExits(string userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId);

            if (cart == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        [HttpPost()]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateCart(CartVm cartVm)
        {
            if (await checkCartExits(cartVm.UserId) ==false)
            {
                var cart = new Cart
                {
                    TotalPrice=0,
                    
                    UserId = cartVm.UserId,
                    Product = new List<Product>()
                };
                var product =await _context.Products.FirstOrDefaultAsync(x => x.Id == cartVm.ProductId);
                var product1 = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
            ,
                    Inventory = product.Inventory,
                    Description = product.Description,
                    BrandId = product.BrandId,
                    CategoryId = product.CategoryId
                };
                cart.Product.Add(product1);
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();

                //return CreatedAtAction("GetCartById", new { id = cart.Id }, new CartVm
                //{
                //    Id = cart.Id,
                //    TotalPrice = cart.TotalPrice,
                //    UserId = ClaimTypes.NameIdentifier

                //});
                return StatusCode(201);
            }

            else
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == cartVm.ProductId);
                var cartVm1 = new CartVm
                {
                    
                };
                var productVm1 = new ProductVm
                {

                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
            ,
                    Inventory = product.Inventory,
                    Description = product.Description,
                    BrandId = product.BrandId,
                    CategoryId = product.CategoryId

                };
                cartVm1.ProductVms.Add(productVm1);

                _ = UpdateCart(cartVm1, cartVm.UserId);
                //return cartVm1;
                return StatusCode(201);
            }
        }
        [HttpPut("userId")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<CartVm>> UpdateCart(CartVm cartVm, string userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId);
            var cartCreate = new Cart
            {
                Id = cartVm.Id,
                TotalPrice = cartVm.TotalPrice,
                UserId = cartVm.UserId
            };
            var pVm = new Product();
            List<Product> lstProducts = new();
            foreach (ProductVm p in cartVm.ProductVms)
            {
                pVm.BrandId = p.BrandId;
                pVm.CategoryId = p.CategoryId;
                pVm.Description = p.Description;
                pVm.Id = p.Id;
                pVm.Inventory = p.Inventory;
                pVm.Name = p.Name;
                pVm.Price = p.Price;
                pVm.Content = p.Content;
                pVm.Quantity = p.Quantity;
                lstProducts.Add(pVm);
            }
            cartCreate.Product = lstProducts;
            _context.Carts.Add(cartCreate);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{userId}/{productId}")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveItem(string userId, int productId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId);
            foreach (Product p in cart.Product)
            {
                if (p.Id == productId)
                {
                    p.Quantity--;
                }
            }
            if (cart == null)
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{userId1}/{productId1}")]
        ////[Authorize(Roles = "User")]
        public async Task<IActionResult> AddItem(string userId1, int productId1)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId1);
            foreach (Product p in cart.Product)
            {
                if (p.Id == productId1)
                {
                    p.Quantity++;
                }
            }
            if (cart == null)
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
