using SimpleJWT.Models;
namespace SimpleJWT.Services
{
    public interface IUserService
    {
        string Login(User user);
    }
}
