using AttributeService.Application.Features.Attributes.Commands.SaveAttribute;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AttributeService.Api.Controllers
{
    [Route("attributes")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttributesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("save")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Save([FromBody] SaveAttributeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
