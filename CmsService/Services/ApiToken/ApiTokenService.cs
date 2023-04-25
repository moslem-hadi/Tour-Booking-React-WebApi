using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Services
{
    public class ApiTokenService : IApiTokenService
    {
        private IConfiguration _configuration { get; }
        private readonly Dapper.IDapperService<object> _dapperService;
        public ApiTokenService(IConfiguration configuration, Dapper.IDapperService<object> dapperService)
        {
            _configuration = configuration;
            _dapperService = dapperService;

        }

        /// <summary>
        /// چک کردن ولید بودن توکن ارسالی در درخواست
        /// </summary>
        /// <param name="token">توکن</param>
        /// <param name="ipOrDomain">آی پی یا دامنه درخواست دهنده</param>
        /// <returns>وضعیت</returns>
        public async Task<bool> IsTokenValid(string token, string ipOrDomain)
        {
            
            var exists = ((int)_dapperService.ExecuteScalar(
                "select count(1) from ApiToken where token=@token and IsActive=1 and ExpirationDate>getdate() and (ValidIpsAndDomains like @ipOrDomain Or ValidIpsAndDomains is null )"
                , new { token, ipOrDomain })) > 0;
            return exists;
        }
    }
}
