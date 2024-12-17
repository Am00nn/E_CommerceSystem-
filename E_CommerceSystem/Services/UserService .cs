using E_CommerceSystem.Models;
using E_CommerceSystem.Repositories;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_CommerceSystem.Services
{
    public class UserService : IUserService
    {
        //private readonly IUserRepository _userRepository;

        //public UserService(IUserRepository userRepository)
        //{
        //    _userRepository = userRepository;
        //}


        //// Register User
        //public User Register(User user)
        //{
        //    // Check if email exists
        //    var foundUser = _userRepository.GetUserByEmail(user.U_Email);

        //    if (foundUser != null)

        //        throw new Exception("Email already used .");

        //    return _userRepository.AddUser(user);
        //}

        //// Login
        //public string Login(string email, string password)
        //{
        //    var user = _userRepository.GetUserByEmail(email);

        //    if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))

        //        throw new Exception("Invalid email or password.");


        //    // Generate a JWT token
        //    return GenerateJwtToken(user);


        //}


        //private string GenerateJwtToken(User user)
        //{
        //    var claims = new[]
        //    {
        //new Claim(ClaimTypes.NameIdentifier, user.UId.ToString()),
        //new Claim(ClaimTypes.Email, user.U_Email),
        //new Claim(ClaimTypes.Role, "User")
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: "your-issuer",
        //        audience: "your-audience",
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(7),
        //        signingCredentials: creds
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}




        //// Get User By ID
        //public User GetUserById(int id)
        //{
        //    var user = _userRepository.GetUserById(id);
        //    if (user == null) throw new Exception("User not found.");
        //    return user;
        //}







        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public string Register(RegisterRequestDto request)
        {
            var existingUser = _userRepository.GetUserByEmail(request.U_Email);
            if (existingUser != null)
                throw new Exception("Email already exists.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                U_Name = request.U_Name,
                U_Email = request.U_Email,
                Password = hashedPassword,
                Phone = request.Phone,
                Role = request.Role,
                CreatedAt = DateTime.UtcNow
            };

            _userRepository.AddUser(user);
            return "User registered successfully.";
        }

        //public string Login(LoginRequestDto request)
        //{
        //    var user = _userRepository.GetUserByEmail(request.U_Email);
        //    if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        //        throw new Exception("Invalid email or password.");

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //            new Claim(ClaimTypes.NameIdentifier, user.UId.ToString()),
        //            new Claim(ClaimTypes.Email, user.U_Email),
        //            new Claim(ClaimTypes.Role, user.Role)
        //        }),
        //        Expires = DateTime.UtcNow.AddHours(2),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}


        public string Login(LoginRequestDto request)
        {
            var user = _userRepository.GetUserByEmail(request.U_Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new Exception("Invalid credentials");

            // Generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]); // Use the same key here

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.UId.ToString()),
            new Claim(ClaimTypes.Email, user.U_Email),
            new Claim(ClaimTypes.Role, user.Role)
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }
    }





}
