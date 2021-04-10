using SharedVm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerSite.Services.Interfaces
{
    public interface IProductApiClient
    {
        Task<IList<ProductVm>> Get();
        Task<ProductVm> GetId(int id);
    }
}
