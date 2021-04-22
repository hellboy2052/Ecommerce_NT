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
        //[Authorize(Roles ="admin")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<OrderDetailVm>>> GetAllOrderdetail()
        {
            return await _context.OrderDetails
                .Select(x => new OrderDetailVm
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<ActionResult<OrderDetailVm>> GetOrderdetailById(int id)
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
                Quantity = order.Quantity,
                UnitPrice = order.UnitPrice
            };

            return orderDetailVm;
        }
        [HttpGet("getOrderDetailByOrderId/{orderId}")]
        //[Authorize(Roles = "user")]
        [AllowAnonymous]
        public async Task<ActionResult<OrderDetailVm>> GetOrderdetailByOrder(int orderId)
        {
            var orderdetail = await _context.OrderDetails.FirstOrDefaultAsync(x => x.OrderId == orderId);

            if (orderdetail == null)
            {
                return NotFound();
            }
            var p = orderdetail.Product;
            var products = await _context.Products.Include(p => p.Images).ToListAsync();
            var pVm = new ProductVm
            {
                Price=p.Price,
                BrandId=p.BrandId,
                CategoryId=p.CategoryId,
                Content=p.Content,
                Description=p.Description,
                Id=p.Id,
                ImageLocation = new List<string>(),
                Inventory =p.Inventory,
                Name=p.Name,
                Quantity=p.Quantity
            };
            for (int i = 0; i < p.Images.Count; i++)
            {
                pVm.ImageLocation.Add(p.Images.ElementAt(i).ImagePath);
            }
            

            var orderDetailVm = new OrderDetailVm
            {
                Id = orderdetail.Id,
                OrderId = orderdetail.OrderId,
                Quantity = orderdetail.Quantity,
                UnitPrice = orderdetail.UnitPrice,
                Product=pVm
            };

            return orderDetailVm;
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "user")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateOrderdetail(int id, OrderDetailVm orderDetailVm)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            orderDetail.Quantity = orderDetailVm.Quantity;
            orderDetail.UnitPrice = orderDetailVm.UnitPrice;

            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpPost]
        //[Authorize(Roles = "user")]
        [AllowAnonymous]
        public async Task<ActionResult<OrderVm>> CreateOrderdetail(OrderDetailVm orderDetailVm)
        {
            var p = orderDetailVm.Product;
            var p1 = new Product
            {
                Price = p.Price,
                BrandId = p.BrandId,
                CategoryId = p.CategoryId,
                Content = p.Content,
                Description = p.Description,
                Id = p.Id,
                Inventory = p.Inventory,
                Name = p.Name,
                Quantity = p.Quantity

            };
            var orderDetail = new OrderDetail
            {
                Id = orderDetailVm.Id,
                OrderId = orderDetailVm.OrderId,
                Quantity = orderDetailVm.Quantity,
                UnitPrice = orderDetailVm.UnitPrice,
                Product=p1
            };

            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = orderDetail.Id }, new OrderDetailVm
            {
                Id = orderDetailVm.Id,
                OrderId = orderDetailVm.OrderId,
                ProductId = orderDetailVm.ProductId,
                Quantity = orderDetailVm.Quantity,
                UnitPrice = orderDetailVm.UnitPrice,
                Product= orderDetailVm.Product
            });
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteOrderdetail(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
