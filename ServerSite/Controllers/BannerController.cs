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
    public class BannerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BannerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BannerVm>>> Get()
        {
            return await _context.Banners
                .Select(x => new BannerVm { Id=x.Id,ImagePath=x.ImagePath,ProductID=x.ProductID})
                .ToListAsync();
        }

        //[HttpGet("{id}")]
        ////[Authorize(Roles ="admin")]
        //public async Task<ActionResult<BrandVm>> GetId(int id)
        //{
        //    var brand = await _context.Brands.FindAsync(id);

        //    if (brand == null)
        //    {
        //        return NotFound();
        //    }

        //    var brandVm = new BrandVm
        //    {
        //        Id = brand.Id,
        //        Name = brand.Name
        //    };

        //    return brandVm;
        //}

        //[HttpPut("{id}")]
        ////[Authorize(Roles = "admin")]
        //public async Task<IActionResult> Put(int id, BrandVm brandVm)
        //{
        //    var brand = await _context.Categories.FindAsync(id);

        //    if (brand == null)
        //    {
        //        return NotFound();
        //    }

        //    brand.Name = brandVm.Name;
        //    await _context.SaveChangesAsync();

        //    return Accepted();
        //}

        //[HttpPost]
        ////[Authorize(Roles = "admin")]
        //public async Task<ActionResult<BrandVm>> Post(BrandVm brandVm)
        //{
        //    var brand = new Brand
        //    {
        //        Name = brandVm.Name
        //    };

        //    _context.Brands.Add(brand);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetBR", new { id = brand.Id }, new BrandVm { Id = brand.Id, Name = brand.Name });
        //}

        //[HttpDelete("{id}")]
        ////[Authorize(Roles = "admin")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var brand = await _context.Brands.FindAsync(id);
        //    if (brand == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Brands.Remove(brand);
        //    await _context.SaveChangesAsync();

        //    return Accepted();
        //}

    }
}
