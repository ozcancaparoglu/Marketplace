using AutoMapper;
using CategoryService.Application.Cache;
using CategoryService.Application.Dtos;
using CategoryService.Domain.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Ocdata.Operations.Cache;
using Ocdata.Operations.Repositories.Contracts;


namespace CategoryService.Application.Services
{
    public class AsyncCategoryService : IAsyncCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        private List<Category> _allCategories;
        public List<Category> AllCategories
        {
            get { return _allCategories; }
            set { _allCategories = value; }
        }

        public AsyncCategoryService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _allCategories = new List<Category>();
        }

        #region Entity Crud

        public async Task<IEnumerable<Category>> List()
        {
            if (!_cacheService.TryGetValue(CacheConstants.CategoryCacheKey, out _allCategories))
            {
                AllCategories = (List<Category>)await _unitOfWork.Repository<Category>().GetAll();
                _cacheService.Add(CacheConstants.CategoryCacheKey, AllCategories, CacheConstants.CategoryCacheTime);
            }

            return AllCategories;
        }

        public async Task<Category?> Save(CategoryDto dto)
        {
            var existing = await _unitOfWork.Repository<Category>().Find(x => x.Name == dto.Name 
            && x.DisplayName == dto.DisplayName);

            if (existing != null)
                return null;

            var entity = _mapper.Map<Category>(dto);

            await _unitOfWork.Repository<Category>().Add(entity);

            _cacheService.Remove(CacheConstants.CategoryCacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }

        public async Task<Category?> Update(CategoryDto dto)
        {
            var entity = await _unitOfWork.Repository<Category>().GetById(dto.Id);

            if (entity == null)
                return null;

            entity.SetCategory(dto.ParentId, dto.Name, dto.DisplayName, dto.Description);

            _unitOfWork.Repository<Category>().Update(entity);

            _cacheService.Remove(CacheConstants.CategoryCacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }

        #endregion

        public async Task<IEnumerable<Category>> ChildCategories(int parentId)
        {
            var subCategories = AllCategories.Where(x => x.ParentId == parentId);

            foreach (var subCategory in subCategories)
            {
                subCategory.SubCategories.AddRange(await ChildCategories(subCategory.Id));
            }

            return subCategories;
        }

    }
}
