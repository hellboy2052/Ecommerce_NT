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
    public class RateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RateController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RateVm>>> Get()
        {
            return await _context.Rates
                .Select(x => new RateVm { Id=x.Id,ProductId=x.ProductId,totalStar=x.totalStar,UserId=x.UserId })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        //[Authorize(Roles ="admin")]
        public async Task<ActionResult<RateVm>> GetId(int id)
        {
            var rate = await _context.Rates.FindAsync(id);

            if (rate == null)
            {
                return NotFound();
            }

            var rateVm = new RateVm
            {
                UserId = rate.UserId,
                totalStar=rate.totalStar,
                ProductId=rate.ProductId,
                Id=rate.Id
            };

            return rateVm;
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Put(int id, RateVm rateVm)
        {
            var rate = await _context.Rates.FindAsync(id);

            if (rate == null)
            {
                return NotFound();
            }

            rate.totalStar = rateVm.totalStar;
            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<RateVm>> Post(RateVm rateVm)
        {
            var rate = new Rate
            {
                totalStar=rateVm.totalStar,
                Id=rateVm.Id,
                ProductId=rateVm.ProductId,
                UserId=rateVm.UserId
            };

            _context.Rates.Add(rate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRate", new { id = rate.Id }, new RateVm { Id = rate.Id,
                totalStar = rate.totalStar,
                ProductId = rate.ProductId,
                UserId = rate.UserId
            });
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var rate = await _context.Rates.FindAsync(id);
            if (rate == null)
            {
                return NotFound();
            }

            _context.Rates.Remove(rate);
            await _context.SaveChangesAsync();

            return Accepted();
        }

    }
}
