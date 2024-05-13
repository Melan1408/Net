using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MovieManager.Contract.Responses;
using MovieManager.Data.Entities.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MovieManager.Service.Commands.Auth
{
    public class LoginCommand
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class LoginCommandHandler : TokenApiBase, IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration) : base (configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByNameAsync(request.UserName) ?? throw new Exception("Invalid username");

            if (!await _userManager.CheckPasswordAsync(user, request.Password)) throw new Exception("Invalid password");

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GenerateToken(authClaims);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(Convert.ToInt16(_configuration["JWTKey:RefreshTokenExpiryTimeInDay"]));

            await _userManager.UpdateAsync(user);

            return new LoginResponse
            {
                UserId = user.Id,
                UserName = user.UserName,
                TokenApiResponse = new TokenApiResponse
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                }
            };
        }
    }
}
