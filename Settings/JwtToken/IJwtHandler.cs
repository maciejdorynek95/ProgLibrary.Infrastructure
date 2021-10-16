using ProgLibrary.Infrastructure.DTO;
using System;

namespace ProgLibrary.Infrastructure.Settings.JwtToken
{
    public interface IJwtHandler
    {
        public JwtDto CreateToken(Guid userId, string role);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
