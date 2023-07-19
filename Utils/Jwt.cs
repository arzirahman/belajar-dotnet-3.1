using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using Coba_Net.Models;
using System.Linq;

namespace Coba_Net.Utils
{

    public class Jwt
    {
        private readonly IConfiguration _configuration;
        private readonly byte[] _secretKey;

        public Jwt(IConfiguration configuration)
        {
            _configuration = configuration;
            string secretKeyPath = configuration["JwtSettings:SecretKeyPath"];
            string secretKey = File.ReadAllText(secretKeyPath);
            _secretKey = Encoding.UTF8.GetBytes(secretKey);
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
            };
            if (!string.IsNullOrEmpty(user.PpUrl))
            {
                claims.Add(new Claim(ClaimTypes.Uri, user.PpUrl));
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(_secretKey);
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationTime"])),
                signingCredentials: signingCredentials
            );

            var tokenString = tokenHandler.WriteToken(tokenOptions);
            return tokenString;
        }

        public User ValidateToken(string token)
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
                var filterClaims = principal.Claims.Where(claim => 
                    claim.Type == ClaimTypes.Email || 
                    claim.Type == ClaimTypes.Name ||
                    claim.Type == ClaimTypes.Uri
                );
                var user = new User
                {
                    Email = filterClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value,
                    Name = filterClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value,
                    PpUrl = filterClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.Uri)?.Value
                };
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
