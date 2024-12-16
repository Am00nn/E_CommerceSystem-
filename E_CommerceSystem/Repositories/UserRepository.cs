using E_CommerceSystem.Models;

namespace E_CommerceSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add User
        public User AddUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // Hash the password
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        // Get All Users
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        // Get User By ID
        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        // Update User
        public User UpdateUser(User user)
        {
            _context.Users.Update(user);

            _context.SaveChanges();

            return user;
        }

        // Delete User
        public bool DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return false;

            _context.Users.Remove(user);

            _context.SaveChanges();

            return true;
        }

        // Get User By Email (For Login)
        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.U_Email == email);
        }


    }
}
