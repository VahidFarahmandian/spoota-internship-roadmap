using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetProject.model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace NetProject.Service
{
    public class TokenService
    {
        public static string GenerateJwtToken(RegisterUser user, IConfiguration configuration)
        {
            var _configuration = configuration;
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JWT:ExpirationInMinutes"]));

            var token = new JwtSecurityToken(
                _configuration["JWT:ValidIssuer"],
                _configuration["JWT:ValidAudience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static byte[] GenerateRandomKey(int sizeInBytes)
        {
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                var keyBytes = new byte[sizeInBytes];
                rng.GetBytes(keyBytes);
                return keyBytes;
            }
        }

    }
}
