using CustomerSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomerSite.Controllers
{
    public class RateController:Controller
    {

            private readonly IRateApiClient _ratingApiClient;

            public RateController(IRateApiClient ratingApiClient)
            {
                _ratingApiClient = ratingApiClient;
            }
            public async Task<IActionResult> Index()
            {
                
                return View("hello");
            }
            [HttpPost]
            public IActionResult Post(int Id, int Star)
            {
                //if (!User.Identity.IsAuthenticated)
                //    return RedirectToAction(actionName: "SignIn", controllerName: "Account");

                RateVm x = new RateVm();
                x.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                x.ProductId = Id;
                x.Star = Star;
                _ratingApiClient.Post(x);

                string referer = Request.Headers["Referer"].ToString();
                return Redirect(referer);

            }
        
    }
}
