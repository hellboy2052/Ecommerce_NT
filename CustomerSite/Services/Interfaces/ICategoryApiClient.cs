using SharedVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerSite.Services.Interfaces
{
    public interface ICategoryApiClient
    {
        Task<IList<CategoryVm>> Get();
    }
}
