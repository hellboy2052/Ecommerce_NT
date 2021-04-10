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
        public async Task<ActionResult<IEnumerable<OrderVm>>> Get()
        {
            return await _context.Orders
                .Select(x => new OrderVm
                {
                    UserId = x.UserId,
                    Address = x.Address,
                    CraeteDate = x.CraeteDate,
                    Id = x.Id,
                    Status = x.Status,
                    totalPrice = x.totalPrice,
                    UserPhone = x.UserPhone
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        //[Authorize(Roles ="admin")]
        public async Task<ActionResult<OrderVm>> GetId(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderVm = new OrderVm
            {
                UserPhone = order.UserPhone,
                totalPrice = order.totalPrice,
                Status = order.Status,
                Id = order.Id,
                Address = order.Address,
                CraeteDate = order.CraeteDate,
                UserId = order.UserId
            };

            return orderVm;
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Put(int id, OrderVm orderVm)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            order.UserId = orderVm.UserId;
            order.totalPrice = orderVm.totalPrice;
            order.Status = orderVm.Status;
            order.Address = orderVm.Address;
            order.CraeteDate = orderVm.CraeteDate;

            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<OrderVm>> Post(OrderVm orderVm)
        {
            var order = new Order
            {
                UserId = orderVm.UserId,
                totalPrice = orderVm.totalPrice,
                Status = orderVm.Status,
                Address = orderVm.Address,
                CraeteDate = orderVm.CraeteDate,
                Id = orderVm.Id,
                UserPhone = orderVm.UserPhone
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOD", new { id = order.Id }, new OrderVm
            {
                UserId = orderVm.UserId,
                totalPrice = orderVm.totalPrice,
                Status = orderVm.Status,
                Address = orderVm.Address,
                CraeteDate = orderVm.CraeteDate,
                Id = orderVm.Id,
                UserPhone = orderVm.UserPhone
            });
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Accepted();
        }

    }
}
