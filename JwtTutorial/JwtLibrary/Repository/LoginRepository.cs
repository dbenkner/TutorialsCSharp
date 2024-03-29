using JwtLibrary.Entities;
using JwtLibrary.ResponseRequestModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JwtLibrary.Repository
{
    public class LoginRepository<TUser> : ILoginRepository<TUser> where TUser : ApplicationUser
    {
        private readonly UserManager<TUser> _userManager;
        private readonly RoleManager<ApplicationRoles> _roleManager;
        private readonly IConfiguration _configuration;

        public LoginRepository(UserManager<TUser> userManager,  RoleManager<ApplicationRoles> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<LoginResponseViewModel> Login(LoginRequestViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    string role = userRoles.FirstOrDefault();
                    await _userManager.AddClaimAsync(user, new Claim("UserRole", role));
                    var authClaims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName), new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), };
                    foreach(var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                    var token = CreateToken(authClaims);
                    var refreshToken = GenerateRefreshToken();
                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInHours"], out int RefreshTokenValidityInHours);
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(RefreshTokenValidityInHours);
                    await _userManager.UpdateAsync(user);
                    return new LoginResponseViewModel
                    {
                        UserId = user.Id,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        IsSuccessful = true,
                        RefreshToken = refreshToken,
                        UserName = user.UserName,
                        Email = user.Email
                    };
                }
                return new LoginResponseViewModel { IsSuccessful = false };
            }catch(Exception ex)
            {
                { throw;  }
            }
        }
        public async Task<bool> SignUp(TUser model, string password)
        {
            try
            {
                var userExist = await _userManager.FindByEmailAsync(model.Email);
                if (userExist != null) return false;
                await _userManager.CreateAsync(model, password);
                await _userManager.AddToRoleAsync(model, "User"); return true;
            } catch(Exception ex) { throw; }
        }
        public async Task<TokenModel> RefreshToken(TokenModel model)
        {
            if (model == null) return new TokenModel { Message = "Invalid Request" };
            var principal = GetPrincipalFromExpiredToken(model.AccessToken);
            if (principal == null) return new TokenModel { Message = "Invalid Refresh Token or Access Token" };
            string username = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new TokenModel { Message = "Invalid Refresh Token or Access Token" };
            }
            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);
            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }
        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInHours"], out int TokenValidityInHours);
            var token = new JwtSecurityToken( issuer: _configuration["JWT:ValidIssuer"], audience: _configuration["JWT:ValidAudience"], expires: DateTime.Now.AddMinutes(TokenValidityInHours));
            return token;
        }
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParmeters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParmeters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) throw new SecurityException("Invalid token");
            return principal;
        }
    }
}
