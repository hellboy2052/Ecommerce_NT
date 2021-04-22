using Microsoft.AspNetCore.Mvc;
using SharedVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerSite.Services.Interfaces
{
    public interface ICartApiClient
    {
        Task<CartVm> Post(CartVm cartVm);
        Task<IList<CartVm>> Get(string userId);
    }
}
