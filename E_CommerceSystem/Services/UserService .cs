using E_CommerceSystem.Models;
using E_CommerceSystem.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_CommerceSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        // Register User
        public User Register(User user)
        {
            // Check if email exists
            var foundUser = _userRepository.GetUserByEmail(user.U_Email);

            if (foundUser != null)

                throw new Exception("Email already used .");

            return _userRepository.AddUser(user);
        }

        // Login
        public string Login(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))

                throw new Exception("Invalid email or password.");


            // Generate a JWT token
            return GenerateJwtToken(user);


        }


        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.UId.ToString()),
        new Claim(ClaimTypes.Email, user.U_Email),
        new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




        // Get User By ID
        public User GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) throw new Exception("User not found.");
            return user;
        }




    }
}
