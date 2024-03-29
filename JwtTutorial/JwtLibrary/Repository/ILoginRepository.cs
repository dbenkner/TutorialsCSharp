using JwtLibrary.Entities;
using JwtLibrary.ResponseRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLibrary.Repository
{
    public interface ILoginRepository<TUser> where TUser: ApplicationUser
    {
        Task<LoginResponseViewModel> Login(LoginRequestViewModel model);
        Task<bool> SignUp(TUser user, string password);
        Task<TokenModel> RefreshToken(TokenModel model);
    }
}
