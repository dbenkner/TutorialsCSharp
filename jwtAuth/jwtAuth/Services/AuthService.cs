﻿using jwtAuth.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jwtAuth.Services
{
    public class AuthService
    {
        public string Create(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var privateKey = Encoding.UTF8.GetBytes(Configuration.PrivateKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(privateKey),
                SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1),
                Subject = GenerateClaims(user)
            };
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim("id", user.Id.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            if(user.Roles != null)
            {
                foreach( var role in user.Roles )
                {
                    var r = role.Role.RoleName;
                    ci.AddClaim(new Claim(ClaimTypes.Role, r));
                }

            }
            return ci;
        }

    }
}
