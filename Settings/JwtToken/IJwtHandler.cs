using Microsoft.AspNetCore.Http;
using ProgLibrary.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Settings.JwtToken
{
    public interface IJwtHandler
    {
        public JwtDto CreateToken(Guid userId, IEnumerable<string> role);
        bool IsTokenValid(string key, string issuer, string token);
        public Task<HttpClient> AddTokenToHeader(HttpClient httpClient,HttpContext httpContext);
    }
}
