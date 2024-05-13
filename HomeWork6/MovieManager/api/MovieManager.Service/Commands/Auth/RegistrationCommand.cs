using Microsoft.AspNetCore.Identity;
using MovieManager.Contract.Responses;
using MovieManager.Data.Entities.Auth;

namespace MovieManager.Service.Commands.Auth
{
    public class RegistrationCommand
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }

    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, RegistrationResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegistrationCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<RegistrationResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken = default)
        {
            var userExists = await _userManager.FindByNameAsync(request.UserName);

            if (userExists != null) throw new Exception("User already exists");

            var user = new ApplicationUser
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
            var createdUserResult = await _userManager.CreateAsync(user, request.Password);

            if (!createdUserResult.Succeeded) throw new Exception("User creation failed! Please check user details and try again.");

            if (!await _roleManager.RoleExistsAsync(request.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(request.Role));
            }

            if (await _roleManager.RoleExistsAsync(request.Role))
            {
                await _userManager.AddToRoleAsync(user, request.Role);
            }

            var registeredUser = await _userManager.FindByNameAsync(request.UserName);

            return new RegistrationResponse
            {
                UserId = registeredUser.Id,
                UserName = registeredUser.UserName,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                Email = registeredUser.Email,
                Role = request.Role
            };
        }
    }
}
