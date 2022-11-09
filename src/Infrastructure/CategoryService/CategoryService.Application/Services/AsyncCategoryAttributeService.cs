using AutoMapper;
using CategoryService.Application.Cache;
using CategoryService.Application.Dtos;
using CategoryService.Domain.CategoryAggregate;
using Ocdata.Operations.Cache;
using Ocdata.Operations.Repositories.Contracts;

namespace CategoryService.Application.Services
{
    public class AsyncCategoryAttributeService : IAsyncCategoryAttributeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public AsyncCategoryAttributeService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task<CategoryAttribute?> Save(CategoryAttributeDto dto)
        {
            var existing = await _unitOfWork.Repository<CategoryAttribute>().Find(x => x.CategoryId == dto.CategoryId && x.AttributeId == dto.AttributeId);

            if (existing != null)
                return null;

            var entity = _mapper.Map<CategoryAttribute>(dto);

            await _unitOfWork.Repository<CategoryAttribute>().Add(entity);

            _cacheService.Remove(CacheConstants.CategoryAttributesCacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }
    }
}
