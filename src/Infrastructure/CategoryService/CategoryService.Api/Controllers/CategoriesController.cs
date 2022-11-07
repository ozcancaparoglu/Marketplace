using CategoryService.Application.ApiContracts.Queries;
using CategoryService.Application.Features.Categories.Commands.SaveCategory;
using CategoryService.Application.Features.Categories.Commands.UpdateCategory;
using CategoryService.Application.Features.Categories.Queries.ListCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ocdata.Operations.Helpers.ResponseHelper;
using System.Net;

namespace CategoryService.Api.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(Result<List<CategoryResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListUsers([FromQuery] ListCategoriesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpPost("save")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Save([FromBody] SaveCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Update([FromBody] UpdateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
