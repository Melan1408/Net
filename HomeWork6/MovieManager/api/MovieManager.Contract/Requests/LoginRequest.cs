using System.ComponentModel.DataAnnotations;

namespace MovieManager.Contract.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
