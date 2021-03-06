using SharedVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerSite.Services.Interfaces
{
    public interface IRateApiClient
    {
        Task<RateVm> CreateRate(RateVm rateVm);
        Task<RateVm> GetRateByProduct(int productId);
    }
}
