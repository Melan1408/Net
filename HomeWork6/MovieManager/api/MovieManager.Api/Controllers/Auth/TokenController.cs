using Microsoft.AspNetCore.Mvc;
using MovieManager.Contract.Requests;
using MovieManager.Contract.Responses;
using MovieManager.Service.Commands.Auth;
using MovieManager.Service;
using Microsoft.AspNetCore.Authorization;

namespace MovieManager.Api.Controllers.Auth
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;

        public TokenController(ILogger<TokenController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken([FromServices] IRequestHandler<RefreshTokenCommand, TokenApiResponse> refreshTokenCommand, [FromBody] TokenApiRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");

                var user = await refreshTokenCommand.Handle(new RefreshTokenCommand
                {
                    AccessToken = request.AccessToken,
                    RefreshToken = request.RefreshToken
                });

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> RevokeToken([FromServices] IRequestHandler<RevokeRefreshTokenApiCommand, bool> revokeRefreshTokenCommand, [FromBody] TokenApiRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");

                var result = await revokeRefreshTokenCommand.Handle(new RevokeRefreshTokenApiCommand
                {
                    AccessToken = request.AccessToken,
                    RefreshToken = request.RefreshToken
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
