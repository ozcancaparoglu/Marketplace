using CategoryService.Application.ApiContracts.Queries;
using MediatR;
using Ocdata.Operations.Helpers.ResponseHelper;

namespace CategoryService.Application.Features.Categories.Queries.ListCategories
{
    public class ListCategoriesQuery : IRequest<Result<List<CategoryResponse>>>
    {
    }
}
