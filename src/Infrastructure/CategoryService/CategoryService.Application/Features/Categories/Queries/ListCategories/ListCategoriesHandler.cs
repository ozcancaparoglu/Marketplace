using AutoMapper;
using CategoryService.Application.ApiContracts.Queries;
using CategoryService.Application.Cache;
using CategoryService.Domain.CategoryAggregate;
using MediatR;
using Ocdata.Operations.Cache;
using Ocdata.Operations.Helpers.ResponseHelper;
using Ocdata.Operations.Repositories.Contracts;

namespace CategoryService.Application.Features.Categories.Queries.ListCategories
{
    public class ListCategoriesHandler : IRequestHandler<ListCategoriesQuery, Result<List<CategoryResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly ICacheService _cacheService;

        //private List<Category> _allCategories;
        //public List<Category> AllCategories
        //{
        //    get { return _allCategories; }
        //    set { _allCategories = value; }
        //}

        public ListCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper/*, ICacheService cacheService*/)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            //_allCategories = new List<Category>();
        }

        public async Task<Result<List<CategoryResponse>>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //if (!_cacheService.TryGetValue(CacheConstants.CategoryCacheKey, out _allCategories))
                //{
                //    AllCategories = (List<Category>)await _unitOfWork.Repository<Category>().GetAll();
                //    _cacheService.Add(CacheConstants.CategoryCacheKey, AllCategories, CacheConstants.CategoryCacheTime);
                //}

                //return await Result<List<CategoryResponse>>.SuccessAsync(_mapper.Map<List<CategoryResponse>>(AllCategories));

                var data = await _unitOfWork.Repository<Category>().GetAll();
                return await Result<List<CategoryResponse>>.SuccessAsync(_mapper.Map<List<CategoryResponse>>(data));
            }
            catch (Exception ex)
            {
                return await Result<List<CategoryResponse>>.FailureAsync(ex.Message);
            }
        }
    }
}
