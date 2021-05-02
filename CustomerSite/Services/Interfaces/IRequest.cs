using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerSite.Services.Interfaces
{
    public interface IRequest
    {
        Task<HttpClient> SendAccessToken();
    }
}
