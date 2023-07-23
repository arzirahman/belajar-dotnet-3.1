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
using Microsoft.AspNetCore.Http;

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

        public ClaimsPrincipal GetClaimsPrincipal(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("Name", user.Name),
                new Claim("Role", user.Role)
            };
            if (!string.IsNullOrEmpty(user.PpUrl))
            {
                claims.Add(new Claim("PpUrl", user.PpUrl));
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
            var cookieClaims = new List<Claim>
            {
                new Claim("Jwt", tokenString),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var claimsIdentity = new ClaimsIdentity(cookieClaims, "Auth");
            return new ClaimsPrincipal(claimsIdentity);
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
                    claim.Type != "exp" && 
                    claim.Type != "iss" &&
                    claim.Type != "aud"
                );
                var user = new User
                {
                    Id = Guid.Parse(filterClaims.FirstOrDefault(claim => claim.Type == "Id")?.Value),
                    Email = filterClaims.FirstOrDefault(claim => claim.Type == "Email")?.Value,
                    Name = filterClaims.FirstOrDefault(claim => claim.Type == "Name")?.Value,
                    PpUrl = filterClaims.FirstOrDefault(claim => claim.Type == "PpUrl")?.Value,
                    Role = filterClaims.FirstOrDefault(claim => claim.Type == "Role")?.Value
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
