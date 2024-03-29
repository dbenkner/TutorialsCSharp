using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLibrary.ResponseRequestModels
{
    public class LoginResponseViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSuccessful { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Email { get; set; }
        public string? Role { get; set; }
    }
}
