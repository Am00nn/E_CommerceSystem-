using E_CommerceSystem.Models;

namespace E_CommerceSystem.Repositories
{
    public interface IUserRepository
    {
        User AddUser(User user);
        bool DeleteUser(int id);
        List<User> GetAllUsers();
        User GetUserByEmail(string email);
        User GetUserById(int id);
        User UpdateUser(User user);
    }
}