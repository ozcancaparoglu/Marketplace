using AttributeService.Application.ApiContracts.Queries;
using MediatR;
using Ocdata.Operations.Helpers.ResponseHelper;

namespace AttributeService.Application.Features.Attributes.Queries.ListAttributes
{
    public class ListAttributesQuery : IRequest<Result<List<AttributeResponse>>>
    {
    }
}
