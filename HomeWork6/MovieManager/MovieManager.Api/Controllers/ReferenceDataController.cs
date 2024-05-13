using Microsoft.AspNetCore.Mvc;
using MovieManager.Contract.Requests;
using MovieManager.Contract.Responses;
using MovieManager.Service.Services;

namespace MovieManager.Api.Controllers
{
	[ApiVersion("1.0")]
	[Route("v{version:apiVersion}/[Controller]")]
	[ApiController]
	public class ReferenceDataController : ControllerBase
	{
		private readonly IRefDataService _refDataService;

		public ReferenceDataController(IRefDataService refDataService)
		{
			_refDataService = refDataService;
		}

        [Route("directors")]
        [HttpGet]
		public async Task<IActionResult> GetDirectorsAsync()
		{
			return Ok(await _refDataService.GetData<DirectorResponse>("Directors"));
		}

        [Route("actors")]
        [HttpGet]
        public async Task<IActionResult> GetActorsAsync()
        {
            return Ok(await _refDataService.GetData<ActorResponse>("Actors"));
        }

        [Route("genres")]
        [HttpGet]
        public async Task<IActionResult> GetGenresAsync()
        {
            return Ok(await _refDataService.GetData<GenreResponse>("Genres"));
        }

        [Route("director")]
        [HttpPost]
		public async Task<IActionResult> PostDirectorAsync([FromBody] UpsertDirectorRequest data)
		{
			return Ok(await _refDataService.PostData("Directors", data));
		}

        [Route("actor")]
        [HttpPost]
        public async Task<IActionResult> PostActorAsync([FromBody] UpsertActorRequest data)
        {
            return Ok(await _refDataService.PostData("Actors", data));
        }

        [Route("genre")]
        [HttpPost]
        public async Task<IActionResult> PostGenreAsync([FromBody] UpsertGenreRequest data)
        {
            return Ok(await _refDataService.PostData("Genres", data));
        }
    }
}
