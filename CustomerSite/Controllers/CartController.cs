using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerSite.Controllers
{
    public class CartController:Controller
    {
        //add Session
        public IActionResult Index()
        {
            List<CartItemsVm> ListPro = HttpContext.Session.Get<List<CartItemsVm>>("SessionCart");
            return View(ListPro);
        }

    }
}
