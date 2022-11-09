using CategoryService.Application.ApiContracts.Queries;
using MediatR;
using Ocdata.Operations.Helpers.ResponseHelper;

namespace CategoryService.Application.Features.Categories.Queries.CategoryTree
{
    public class CategoryTreeQuery : IRequest<Result<List<CategoryTreeResponse>>>
    {
    }
}
