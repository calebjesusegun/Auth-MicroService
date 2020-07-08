using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SRC.Auth.API.Interface;
using SRC.Auth.API.Models;
using SRC.Auth.Data.Entities;

namespace SRC.Auth.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> RegisterUser(RegisterModel model)
        {
            if (model is null) throw new ArgumentNullException(message: "Invalid details provided", null);

            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(message: AddErrors(result), null);
                }
            }

            return user.UserName;
        }

        private string AddErrors(IdentityResult result)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var error in result.Errors)
            {
                sb.Append(error.Description + " ");
            }
            return sb.ToString();
        }
    }
}