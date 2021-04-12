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
    public class RateController
    {
        public class RatingController : Controller
        {
            private readonly IRateApiClient _ratingApiClient;

            public RatingController(IRateApiClient ratingApiClient)
            {
                _ratingApiClient = ratingApiClient;
            }

            [HttpPost]
            public IActionResult CreateRate(int ProductId, int Star)
            {
                //if (!User.Identity.IsAuthenticated)
                //    return RedirectToAction(actionName: "SignIn", controllerName: "Account");

                RateVm x = new RateVm();
                x.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                x.ProductId = ProductId;
                x.Star = Star;
                _ratingApiClient.CreateRate(x);

                string referer = Request.Headers["Referer"].ToString();
                return Redirect(referer);

            }
        }
    }
}
