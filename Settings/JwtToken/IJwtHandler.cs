using ProgLibrary.Infrastructure.DTO;
using System;
using System.Collections.Generic;

namespace ProgLibrary.Infrastructure.Settings.JwtToken
{
    public interface IJwtHandler
    {
        public JwtDto CreateToken(Guid userId, IEnumerable<string> role);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
