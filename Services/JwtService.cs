using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Coba_Net.Services
{
    public interface IJwtService
    {
        string GenerateToken(ClaimsIdentity claimsIdentity);
        ClaimsPrincipal ValidateToken(string token);
    }

    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly byte[] _secretKey;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            string secretKeyPath = configuration["JwtSettings:SecretKeyPath"];
            string secretKey = File.ReadAllText(secretKeyPath);
            _secretKey = Encoding.UTF8.GetBytes(secretKey);
        }

        public string GenerateToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(_secretKey);
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claimsIdentity.Claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationTime"])),
                signingCredentials: signingCredentials
            );

            var tokenString = tokenHandler.WriteToken(tokenOptions);
            return tokenString;
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var key = new SymmetricSecurityKey(_secretKey);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["JwtSettings:Issuer"],
                    ValidAudience = _configuration["JwtSettings:Audience"],
                    IssuerSigningKey = key
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
