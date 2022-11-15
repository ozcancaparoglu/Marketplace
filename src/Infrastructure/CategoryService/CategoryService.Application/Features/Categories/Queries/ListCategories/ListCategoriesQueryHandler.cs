using AutoMapper;
using CategoryService.Application.ApiContracts.Queries;
using CategoryService.Application.Services;
using MediatR;
using Ocdata.Operations.Helpers.ResponseHelper;

namespace CategoryService.Application.Features.Categories.Queries.ListCategories
{
    public class ListCategoriesQueryHandler : IRequestHandler<ListCategoriesQuery, Result<List<CategoryResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncCategoryService _categoryService;

        public ListCategoriesQueryHandler(IMapper mapper, IAsyncCategoryService categoryService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public async Task<Result<List<CategoryResponse>>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await Result<List<CategoryResponse>>.SuccessAsync(_mapper.Map<List<CategoryResponse>>(await _categoryService.List()));
        }
    }
}
