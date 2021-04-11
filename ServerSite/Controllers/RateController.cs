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
    public class RateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RateController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RateVm>>> GetAllRate()
        {
            return await _context.Rates
                .Select(x => new RateVm { Id = x.Id, ProductId = x.ProductId, Star = x.Star, UserId = x.UserId })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<RateVm>> GetRateById(int id)
        {
            var rate = await _context.Rates.FindAsync(id);

            if (rate == null)
            {
                return NotFound();
            }

            var rateVm = new RateVm
            {
                UserId = rate.UserId,
                Star = rate.Star,
                ProductId = rate.ProductId,
                Id = rate.Id,
                Average=rate.Average
            };

            return rateVm;
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<RateVm>> GetRateByUserId(string userId)
        {
            var rate = await _context.Rates.FirstOrDefaultAsync(x => x.UserId == userId);

            if (rate == null)
            {
                return NotFound();
            }

            var rateVm = new RateVm
            {
                UserId = rate.UserId,
                Star = rate.Star,
                ProductId = rate.ProductId,
                Id = rate.Id,
                Average = rate.Average
            };

            return rateVm;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> UpdateRateByUserId(string userId, RateVm rateVm)
        {
            var rate = await _context.Rates.FirstOrDefaultAsync(x => x.UserId == userId);

            if (rate == null)
            {
                return NotFound();
            }

            rate.Star = rateVm.Star;
            rate.Average = rateVm.Average;
            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<RateVm>> CreateRate(RateVm rateVm)
        {
            var rate = new Rate
            {
                Star = rateVm.Star,
                Id = rateVm.Id,
                ProductId = rateVm.ProductId,
                UserId = rateVm.UserId
            };

            _context.Rates.Add(rate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRateById", new { id = rate.Id }, new RateVm
            {
                Id = rate.Id,
                Star = rate.Star,
                ProductId = rate.ProductId,
                UserId = rate.UserId
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
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
