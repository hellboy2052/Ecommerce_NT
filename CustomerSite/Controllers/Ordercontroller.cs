using CustomerSite.Models;
using CustomerSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
namespace CustomerSite.Controllers
{
    public class Ordercontroller : Controller
    {
        private readonly IOrderApiClient _orderApiClient;
        private readonly IConfiguration _configuration;
        private readonly ICartApiClient _cartApiClient;

        public Ordercontroller( IOrderApiClient orderApiClient, IConfiguration configuration)
        {
            _orderApiClient = orderApiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderApiClient.GetOrderByUser(userId);
            //Set url backend for image

            return View(orders);

        }
        //AddCartItem(string userId, List<OrderDetailVm> orderDetailVm1)
    }
}
