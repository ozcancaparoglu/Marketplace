using CategoryService.Application.Features.CategoryAttributes.Commands.SaveCategoryAttribute;
using CategoryService.Application.Features.CategoryAttributes.Commands.UpdateCategoryAttribute;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CategoryService.Api.Controllers
{
    [Route("categoryAttributes")]
    [ApiController]
    public class CategoryAttributesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryAttributesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("save")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Save([FromBody] SaveCategoryAttributeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Update([FromBody] UpdateCategoryAttributeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
