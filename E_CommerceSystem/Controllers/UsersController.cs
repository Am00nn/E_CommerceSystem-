using E_CommerceSystem.Models;
using E_CommerceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystem.Controllers
{


    //[ApiController]
    //[Route("api/users")]
    //public class UsersController : ControllerBase
    //{
    //    private readonly IUserService _userService;

    //    // Constructor to inject the user service
    //    public UsersController(IUserService userService)
    //    {
    //        _userService = userService;


    //    }

    //    //Endpoint for user registration
    //    [HttpPost("register")]
    //    public IActionResult Register([FromBody] User user)
    //    {
    //        try
    //        {
    //            var registeredUser = _userService.Register(user);

    //            return Ok(registeredUser);

    //        }
    //        catch (Exception ex)
    //        {
    //            // Returns = 400 (Bad Request) in case of failure
    //            return BadRequest(ex.Message);
    //        }





    //    }

    //    // Endpoint for user login

    //    // Expects a LoginRequest object (Email and Password) in the request body
    //    [HttpPost("login")]
    //    public IActionResult Login([FromBody] LoginRequest loginRequest)
    //    {
    //        try
    //        {
    //            // Calls the Login method in IUserService to validate 

    //            var token = _userService.Login(loginRequest.Email, loginRequest.Password);

    //            // Returns a JSON object containing the JWT token with status 200 (OK)
    //            return Ok(new { Token = token });
    //        }
    //        catch (Exception ex)
    //        {
    //            // Returns  401 (Unauthorized) in case of invalid 
    //            return Unauthorized(ex.Message);
    //        }
    //    }


    //    // Endpoint to get user details by their ID

    //    // This endpoint is restricted to authenticated users via [Authorize] attribute
    //    [HttpGet("{id}")]
    //    [Authorize]
    //    public IActionResult GetUserById(int id)
    //    {
    //        try
    //        {
    //            // Calls the GetUserById 

    //            var user = _userService.GetUserById(id);

    //            // Returns the user details  
    //            return Ok(user);
    //        }
    //        catch (Exception ex)
    //        {

    //            return NotFound(ex.Message);
    //        }
    //    }
    //}

    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto request)
        {
            try
            {
                var result = _userService.Register(request);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var token = _userService.Login(request);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            return Ok(user);
        }
    }








}
