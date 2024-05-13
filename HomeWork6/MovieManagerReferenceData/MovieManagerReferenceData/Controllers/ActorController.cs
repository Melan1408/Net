using Microsoft.AspNetCore.Mvc;
using MovieManagerReferenceData.Contract.Requests;
using MovieManagerReferenceData.Contract.Responses;
using MovieManagerReferenceData.Service;
using MovieManagerReferenceData.Service.Actors.Commands;

namespace MovieManagerReferenceData.Api.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetActorsAsync([FromServices] IRequestHandler<IList<ActorResponse>> getActorsQuery)
        {
            return Ok(await getActorsQuery.Handle());
        }

        [HttpPost]
        public async Task<IActionResult> UpsertActorAsync([FromServices] IRequestHandler<UpsertActorCommand, ActorResponse> upsertActorCommand, [FromBody] UpsertActorRequest request)
        {
            var actor = await upsertActorCommand.Handle(new UpsertActorCommand
            {
               ActorId = request.ActorId,
               Name = request.Name,
               Surname = request.Surname,
               Age = request.Age
            });

            return Ok(actor);
        }

        [HttpDelete("{actorId}")]
        public async Task<IActionResult> DeleteActorById(int actorId, [FromServices] IRequestHandler<DeleteActorCommand, bool> deleteActorByIdCommand)
        {
            var result = await deleteActorByIdCommand.Handle(new DeleteActorCommand { ActorId = actorId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
