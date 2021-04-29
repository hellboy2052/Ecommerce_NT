using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerSite.Data;
using ServerSite.Models;
using SharedVm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetAllProduct()
        {
            var products = await _context.Products.Include(p => p.Images).ToListAsync();
            if (products == null)
            {
                return NotFound();
            }
            List<ProductVm> productListVm = new();
            foreach (var product in products)
            {
                ProductVm productVm = new()
                {

                   
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    Id = product.Id,
                    Inventory = product.Inventory,
                    Name = product.Name,
                    Price = product.Price,
                    ImageLocation = new List<string>()
                };
                for (int i = 0; i < product.Images.Count; i++)
                {
                    productVm.ImageLocation.Add(product.Images.ElementAt(i).ImagePath);
                }
                productListVm.Add(productVm);
            }
            return productListVm;
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductVm>> GetProductById(int id)
        {
                var product = await _context.Products.Include(p => p.Images).Include(p=>p.Rates).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var productVm = new ProductVm
            {
                Name = product.Name,
                Id = product.Id,
              
                CategoryId = product.CategoryId,
                Description = product.Description,
                Inventory = product.Inventory,
                Price = product.Price,
                ImageLocation = new List<string>()
            };
            for (int i = 0; i < product.Images.Count; i++)
            {
                productVm.ImageLocation.Add(product.Images.ElementAt(i).ImagePath);
            }
            int temp = 0;
            if (product.Rates.Count == 0)
            {
                productVm.AverageStar = 0;
            }
            else { 
            foreach (var y in product.Rates)
            {
                temp += y.Star;
            }
            productVm.AverageStar = temp / product.Rates.Count();
            }
            return productVm;
        }
        [HttpGet("getByCategoryId/{idCategory}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProductByCategory(int idCategory)
        {
            var products = await _context.Products.Include(p => p.Images).Where(p => p.CategoryId == idCategory).ToListAsync();
            List<ProductVm> productListVm = new();
            foreach (var product in products)
            {
                ProductVm productVm = new()
                {

                   
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    Id = product.Id,
                    Inventory = product.Inventory,
                    Name = product.Name,
                    Price = product.Price,
                    ImageLocation = new List<string>()
                };
                for (int i = 0; i < product.Images.Count; i++)
                {
                    productVm.ImageLocation.Add(product.Images.ElementAt(i).ImagePath);
                }
                productListVm.Add(productVm);
            }
            return productListVm;
        }
        [HttpPut]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateProduct(ProductVm productVm)
        {
            var id = productVm.Id;
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = productVm.Name;
            
            product.CategoryId = productVm.CategoryId;
            product.Description = productVm.Description;
            product.Inventory = productVm.Inventory;
            product.Price = productVm.Price;
            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        public async Task<ActionResult<ProductVm>> CreateProduct(ProductVm productVm)
        {
            var product = new Product
            {
                Name = productVm.Name,
                //Id = productVm.Id,
                AverageStar=productVm.AverageStar,
                CategoryId = productVm.CategoryId,
                Description = productVm.Description,
                Inventory = productVm.Inventory,
                Price = productVm.Price
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = productVm.Id }, new ProductVm
            {
                Name = product.Name,
                AverageStar = product.AverageStar,
                CategoryId = product.CategoryId,
                Description = product.Description,
               
                Inventory = product.Inventory,
                Price = product.Price,
            });
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Accepted();
        }

    }
}
