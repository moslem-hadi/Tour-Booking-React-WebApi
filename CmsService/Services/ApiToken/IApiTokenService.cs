using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Services
{
   public interface IApiTokenService
    {
        Task<bool> IsTokenValid(string api, string url);
    }
}
