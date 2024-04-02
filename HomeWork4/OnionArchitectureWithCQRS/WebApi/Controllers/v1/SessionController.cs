using Application.ProductFeatures.Commands;
using Application.ProductFeatures.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SessionController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetSessionAsync()
        {
            return Ok(await Mediator.Send(new GetSessionsQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSessionAsync(CreateSessionCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateSessionAsync(int sessionId, UpdateSessionCommand command)
        {
            try
            {
                if (sessionId != command.SessionId)
                {
                    return BadRequest();
                }

                await Mediator.Send(command);

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
