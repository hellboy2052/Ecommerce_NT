using SharedVm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerSite.Services.Interfaces
{
    public interface IBannerApiClient
    {
        Task<IList<BannerVm>> GetAllBanner();
    }
}
