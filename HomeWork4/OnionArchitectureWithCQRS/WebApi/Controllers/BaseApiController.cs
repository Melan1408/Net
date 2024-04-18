using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        public IMediator Mediator
        {
            get => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
            set
            {
                _mediator = value;
            }
        }
    }
}
