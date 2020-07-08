using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SRC.Auth.API.Interface;
using SRC.Auth.API.Models;

namespace SRC.Auth.API.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(
            IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("/register")]//localhost:5000/register
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid) throw new ArgumentNullException(message: "Invalid Details Provided", null);

                var userName = await _authService.RegisterUser(model);
                return new CreatedResult("/register/", new { Username = userName, message = "User account created successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        // [HttpGet("/login")]
        // public IActionResult Login()
        // {
        //     throw new NotImplementedException();
        // }

        // [HttpGet("/logout")]
        // public IActionResult Logout()
        // {
        //     throw new NotImplementedException();
        // }
    }
}