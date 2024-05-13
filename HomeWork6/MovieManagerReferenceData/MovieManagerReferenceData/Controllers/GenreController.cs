using Microsoft.AspNetCore.Mvc;
using MovieManagerReferenceData.Contract.Requests;
using MovieManagerReferenceData.Contract.Responses;
using MovieManagerReferenceData.Service;
using MovieManagerReferenceData.Service.Actors.Commands;
using MovieManagerReferenceData.Service.Genres.Commands;

namespace MovieManagerReferenceData.Api.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetGenresAsync([FromServices] IRequestHandler<IList<GenreResponse>> getGenresQuery)
        {
            return Ok(await getGenresQuery.Handle());
        }

        [HttpPost]
        public async Task<IActionResult> UpsertGenreAsync([FromServices] IRequestHandler<UpsertGenreCommand, GenreResponse> upsertGenreCommand, [FromBody] UpsertGenreRequest request)
        {
            var genre = await upsertGenreCommand.Handle(new UpsertGenreCommand
            {
               GenreId = request.GenreId,
               Name = request.Name
            });

            return Ok(genre);
        }

        [HttpDelete("{genreId}")]
        public async Task<IActionResult> DeleteGenreById(int genreId, [FromServices] IRequestHandler<DeleteGenreCommand, bool> deleteGenreByIdCommand)
        {
            var result = await deleteGenreByIdCommand.Handle(new DeleteGenreCommand { GenreId = genreId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
