using Microsoft.AspNetCore.Mvc;
using SharedVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerSite.Services.Interfaces
{
    public interface IOrderApiClient
    {
        Task<IList<OrderVm>> GetOrderByUser(string userId);
        Task<OrderVm> CreateOrder(string userId, List<OrderDetailVm> orderDetailVm1);
    }
}
