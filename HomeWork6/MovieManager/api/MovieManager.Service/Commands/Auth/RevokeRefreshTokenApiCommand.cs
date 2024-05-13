using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MovieManager.Data.Entities.Auth;

namespace MovieManager.Service.Commands.Auth
{
    public class RevokeRefreshTokenApiCommand
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }

    public class RevokeRefreshTokenApiCommandHandler : TokenApiBase, IRequestHandler<RevokeRefreshTokenApiCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RevokeRefreshTokenApiCommandHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration) : base(configuration)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(RevokeRefreshTokenApiCommand command, CancellationToken cancellationToken = default)
        {
            var principal = GetPrincipalFromExpiredToken(command.AccessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            var user = await _userManager.FindByNameAsync(username) ?? throw new Exception("Invalid username");

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return true;
        }
    }
}