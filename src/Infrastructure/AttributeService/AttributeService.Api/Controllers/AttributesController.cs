using AttributeService.Application.ApiContracts.Queries;
using AttributeService.Application.Features.Attributes.Commands.SaveAttribute;
using AttributeService.Application.Features.Attributes.Commands.UpdateAttribute;
using AttributeService.Application.Features.Attributes.Queries.ListAttributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ocdata.Operations.Helpers.ResponseHelper;
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

        [HttpGet("list")]
        [ProducesResponseType(typeof(Result<List<AttributeResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new ListAttributesQuery());
            return Ok(result);
        }

        [HttpPost("save")]
        [ProducesResponseType(typeof(Result<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Save([FromBody] SaveAttributeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("update")]
        [ProducesResponseType(typeof(Result<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Update([FromBody] UpdateAttributeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
