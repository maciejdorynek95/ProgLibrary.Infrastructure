using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProgLibrary.Infrastructure.DTO;
using ProgLibrary.Infrastructure.Extensions;
using ProgLibrary.Infrastructure.Settings.JwtToken;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ProgLibrary.Infrastructure.Services.JwtToken
{
    public class JwtHandler : IJwtHandler
    {
        //private static JwtSettings settings;
        //public static JwtSettings Settings { set { settings = value; } }

        private readonly JwtSettings _jwtSettings;
        public JwtHandler(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public JwtDto CreateToken(Guid userId, IEnumerable<string> roles)
        {
            var now = DateTime.Now;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // unikalny id tokena
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimeStamp().ToString()) // data wydania tokena

            };foreach (var role in roles) {claims.Add(new Claim(ClaimTypes.Role, role));}


            var expires = now.AddMinutes(_jwtSettings.ExpiryMinutes);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,               
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
          
            return new JwtDto
            {
                Token = token,
                Expires = expires.ToTimeStamp()
            };
        }

        public bool IsTokenValid(string key, string issuer, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
