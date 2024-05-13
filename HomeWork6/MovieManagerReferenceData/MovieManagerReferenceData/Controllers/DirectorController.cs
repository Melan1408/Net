using Microsoft.AspNetCore.Mvc;
using MovieManagerReferenceData.Contract.Requests;
using MovieManagerReferenceData.Contract.Responses;
using MovieManagerReferenceData.Service;
using MovieManagerReferenceData.Service.Actors.Commands;
using MovieManagerReferenceData.Service.Directors.Commands;

namespace MovieManagerReferenceData.Api.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetDirectorsAsync([FromServices] IRequestHandler<IList<DirectorResponse>> getDirectorsQuery)
        {
            return Ok(await getDirectorsQuery.Handle());
        }

        [HttpPost]
        public async Task<IActionResult> UpsertDirectorAsync([FromServices] IRequestHandler<UpsertDirectorCommand, DirectorResponse> upsertDirectorCommand, [FromBody] UpsertDirectorRequest request)
        {
            var director = await upsertDirectorCommand.Handle(new UpsertDirectorCommand
            {
               DirectorId = request.DirectorId,
               Name = request.Name,
               Surname = request.Surname,
               Age = request.Age
            });

            return Ok(director);
        }

        [HttpDelete("{directorId}")]
        public async Task<IActionResult> DeleteActorById(int directorId, [FromServices] IRequestHandler<DeleteDirectorCommand, bool> deleteDirectorByIdCommand)
        {
            var result = await deleteDirectorByIdCommand.Handle(new DeleteDirectorCommand { DirectorId = directorId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
