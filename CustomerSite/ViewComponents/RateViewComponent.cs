using CustomerSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedVm;
using System.Threading.Tasks;

namespace CustomerSite.ViewComponents
{
    public class RateViewComponent : ViewComponent
    {
        private readonly IRateApiClient _rateApiClient;

        public RateViewComponent(IRateApiClient rateApiClient)
        {
            _rateApiClient = rateApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(RateVm rateVm)
        {
            var rate = await _rateApiClient.CreateRate(rateVm);

            return View(rate);
        }
    }
}
