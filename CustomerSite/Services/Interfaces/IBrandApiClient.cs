using SharedVm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerSite.Services.Interfaces
{
    public interface IBrandApiClient
    {
        Task<IList<BrandVm>> Get();
    }
}
