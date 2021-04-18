using Microsoft.AspNetCore.Mvc;
using SharedVm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerSite.Services.Interfaces
{
    public interface IProductApiClient
    {
        Task<IList<ProductVm>> Get();
        Task<ProductVm> Get(int id);
        Task<IList<ProductVm>> Get1(int idCategory);
    }
}
