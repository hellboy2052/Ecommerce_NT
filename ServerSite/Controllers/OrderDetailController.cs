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
    public class OrderDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<OrderDetailVm>>> Get()
        {
            return await _context.OrderDetails
                .Select(x => new OrderDetailVm
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        //[Authorize(Roles ="admin")]
        public async Task<ActionResult<OrderDetailVm>> GetId(int id)
        {
            var order = await _context.OrderDetails.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDetailVm = new OrderDetailVm
            {
                Id = order.Id,
                OrderId = order.OrderId,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                UnitPrice = order.UnitPrice
            };

            return orderDetailVm;
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Put(int id, OrderDetailVm orderDetailVm)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            orderDetail.ProductId = orderDetailVm.ProductId;
            orderDetail.Quantity = orderDetailVm.Quantity;
            orderDetail.UnitPrice = orderDetailVm.UnitPrice;

            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<OrderVm>> Post(OrderDetailVm orderDetailVm)
        {
            var orderDetail = new OrderDetail
            {
                Id = orderDetailVm.Id,
                OrderId = orderDetailVm.OrderId,
                ProductId = orderDetailVm.ProductId,
                Quantity = orderDetailVm.Quantity,
                UnitPrice = orderDetailVm.UnitPrice,

            };

            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetODD", new { id = orderDetail.Id }, new OrderDetailVm
            {
                Id = orderDetailVm.Id,
                OrderId = orderDetailVm.OrderId,
                ProductId = orderDetailVm.ProductId,
                Quantity = orderDetailVm.Quantity,
                UnitPrice = orderDetailVm.UnitPrice,
            });
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return Accepted();
        }

    }
}
