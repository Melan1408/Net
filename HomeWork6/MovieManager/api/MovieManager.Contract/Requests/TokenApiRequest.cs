using System.ComponentModel.DataAnnotations;

namespace MovieManager.Contract.Requests
{
    public class TokenApiRequest
    {
        [Required(ErrorMessage = "Access Token is required")]
        public string AccessToken { get; set; }

        [Required(ErrorMessage = "Refresh Token is required")]
        public string RefreshToken { get; set; }
    }
}
