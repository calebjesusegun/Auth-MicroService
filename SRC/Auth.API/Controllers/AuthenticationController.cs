using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SRC.Auth.API.Interface;
using SRC.Auth.API.Models;
using SRC.Auth.Data.Entities;

namespace SRC.Auth.API.Controllers {
    [ApiController]
    public class AuthenticationController : ControllerBase {
        private readonly IAuthService _authService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationController (
            IAuthService authService,
            SignInManager<ApplicationUser> signInManager) {
            _authService = authService;
            _signInManager = signInManager;
        }

        [HttpPost ("/register")] //localhost:5000/register
        public async Task<IActionResult> Register (RegisterModel model) {
            try {
                if (!ModelState.IsValid) throw new ArgumentNullException (message: "Invalid Details Provided", null);

                var userName = await _authService.RegisterUser (model);
                return new CreatedResult ("/register/", new { Username = userName, message = "User account created successfully" });
            } catch (Exception e) {
                return BadRequest (new { message = e.Message });
            }
        }

        [HttpPost ("/login")]
        public async Task<IActionResult> Login (LoginModel model) {
            try {
                if (!ModelState.IsValid) throw new ArgumentNullException (message: "Invalid Details Provided", null);

                var result = await _signInManager.PasswordSignInAsync (model.UserName, model.Password, false, false);

                if (!result.Succeeded) return BadRequest (new { message = "Invalid details provided" });

                return Ok (result);

            } catch (Exception e) {
                return BadRequest (new { message = e.Message });
            }
        }

        [HttpGet ("/logout")]
        public async Task<IActionResult> Logout () {
            try {
                await _signInManager.SignOutAsync ();
                return Ok ();
            } catch (Exception e) {
                return BadRequest (new { message = e.Message });
            }
        }
    }
}