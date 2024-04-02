using Application.ProductFeatures.Commands;
using Application.ProductFeatures.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MovieController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetMoviesAsync()
        {
            return Ok(await Mediator.Send(new GetMoviesQuery()));
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetMovieByIdAsync(int movieId)
        {
            try
            {
                var result = await Mediator.Send(new GetMovieByIdQuery { MovieId = movieId });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateMovieAsync(CreateMovieCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateMovieAsync(int movieId, UpdateMovieCommand command)
        {
            try
            {
                if (movieId != command.MovieId)
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

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> DeleteMovieById(int movieId)
        {         
            try
            {
                await Mediator.Send(new DeleteMovieCommand { MovieId = movieId });

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
