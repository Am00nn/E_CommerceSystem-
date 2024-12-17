using E_CommerceSystem.Models;

namespace E_CommerceSystem.Services
{
    public interface IUserService
    {
        User GetUserById(int id);
        string Login(LoginRequestDto request);
        string Register(RegisterRequestDto request);
    }
}