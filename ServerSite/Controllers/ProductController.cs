using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> Get()
        {
            return await _context.Products
                .Select(x => new ProductVm { Name = x.Name,Id=x.Id,BrandId=x.BrandId,
                    CategoryId=x.CategoryId,Description=x.Description,Inventory=x.Inventory,Price=x.Price })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        //[Authorize(Roles ="admin")]
        public async Task<ActionResult<ProductVm>> GetId(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productVm = new ProductVm
            {
                Name = product.Name,
                Id = product.Id,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Inventory = product.Inventory,
                Price = product.Price
            };

            return productVm;
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Put(int id, ProductVm productVm)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = productVm.Name;
            product.BrandId = productVm.BrandId;
            product.CategoryId = productVm.CategoryId;
            product.Description = productVm.Description;
            product.Inventory = productVm.Inventory;
            product.Price = productVm.Price;
            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<ProductVm>> Post(ProductVm productVm)
        {
            var product = new Product
            {
                Name = productVm.Name,
                Id = productVm.Id,
                BrandId = productVm.BrandId,
                CategoryId = productVm.CategoryId,
                Description = productVm.Description,
                Inventory = productVm.Inventory,
                Price = productVm.Price
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPR", new { id = product.Id }, new ProductVm { Id = product.Id, Name = product.Name,Price=product.Price
            ,Inventory=product.Inventory,Description=product.Description,BrandId=product.BrandId,CategoryId=product.CategoryId});
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
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
