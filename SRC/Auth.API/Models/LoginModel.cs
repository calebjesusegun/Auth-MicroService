using System.ComponentModel.DataAnnotations;

namespace SRC.Auth.API.Models {
    public class LoginModel {
        [Required]
        [Display (Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}