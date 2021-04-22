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
    //[Authorize("Bearer")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<OrderVm>>> GetAllOrder()
        {
            return await _context.Orders.Include(o=>o.OrderDetails)
                .Select(x => new OrderVm
                {
                    UserId = x.UserId,
                    CraeteDate = x.CraeteDate,
                    Id = x.Id,
                    Status = x.Status,
                    TotalPrice=x.TotalPrice
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<ActionResult<OrderVm>> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderVm = new OrderVm
            {
                Status = order.Status,
                Id = order.Id,
                CraeteDate = order.CraeteDate,
                UserId = order.UserId,
                TotalPrice=order.TotalPrice
            };

            return orderVm;
        }
        [HttpGet("getOrderByUser/{userId}")]
        //[Authorize(Roles = "user")]
        public async Task<ActionResult<OrderVm>> GetOrderByUser(string userId)
        {
            var order = await _context.Orders.Include(o=>o.OrderDetails).FirstOrDefaultAsync(x => x.UserId == userId);

            if (order == null)
            {
                return NotFound();
            }

            var orderVm = new OrderVm
            {
                Status = order.Status,
                Id = order.Id,
                CraeteDate = order.CraeteDate,
                UserId = order.UserId,
                TotalPrice = order.TotalPrice
                
            };

            return orderVm;
        }
        [HttpPut("{id}")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateOrder(int id, OrderVm orderVm)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            order.TotalPrice = orderVm.TotalPrice;
            order.Status = orderVm.Status;
            order.CraeteDate = orderVm.CraeteDate;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        //[Authorize(Roles = "user")]
        [AllowAnonymous]
        public async Task<ActionResult<OrderVm>> CreateOrder(OrderVm orderVm)
        {
            var order = new Order
            {
                UserId = orderVm.UserId,
                TotalPrice = orderVm.TotalPrice,
                Status = orderVm.Status,
                CraeteDate = orderVm.CraeteDate,
                Id = orderVm.Id
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = order.Id }, new OrderVm
            {
                UserId = orderVm.UserId,
                TotalPrice = orderVm.TotalPrice,
                Status = orderVm.Status,
                CraeteDate = orderVm.CraeteDate,
                Id = orderVm.Id,
            });
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
