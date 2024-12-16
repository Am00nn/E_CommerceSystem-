using E_CommerceSystem.Models;

namespace E_CommerceSystem.Services
{
    public interface IUserService
    {
        User GetUserById(int id);
        string Login(string email, string password);
        User Register(User user);
    }
}