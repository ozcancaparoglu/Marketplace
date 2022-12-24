using AutoMapper;
using CategoryService.Application.Cache;
using CategoryService.Application.Dtos;
using CategoryService.Application.Services.BaseServices;
using CategoryService.Domain.CategoryAggregate;
using Ocdata.Operations.Cache;
using Ocdata.Operations.Helpers.ResponseHelper;
using Ocdata.Operations.Repositories.Contracts;


namespace CategoryService.Application.Services
{
    public class AsyncCategoryService : CrudBaseService<Category,CategoryDto>, IAsyncCategoryService
    {
        public AsyncCategoryService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService) 
            : base(unitOfWork, mapper, cacheService)
        {
        }

        #region Entity Crud

        public async Task<IEnumerable<Category>> List() => await List(new PagerInput(), all: true, cacheKey: CacheConstants.CategoryCacheKey, cacheTime: CacheConstants.CategoryCacheTime);
        public async Task<Category?> Save(CategoryDto dto) => await Save(dto, filter: x => x.Name == dto.Name && x.DisplayName == dto.DisplayName, cacheKey: CacheConstants.CategoryCacheKey);
        public async Task<Category?> Update(CategoryDto dto)
        {
            var entity = await _unitOfWork.Repository<Category>().GetById(dto.Id);

            if (entity == null)
                return null;

            entity.SetCategory(dto.ParentId, dto.Name, dto.DisplayName, dto.Description);

            return await Update(entity, cacheKey: CacheConstants.CategoryCacheKey);
        }
        public async Task<bool> Delete(CategoryDto dto) => await Delete(dto, cacheKey: CacheConstants.CategoryCacheKey);

        #endregion

        public async Task<IEnumerable<Category>> ChildCategories(int parentId)
        {
            var subCategories = AllEntities.Where(x => x.ParentId == parentId);

            foreach (var subCategory in subCategories)
            {
                subCategory.SubCategories.AddRange(await ChildCategories(subCategory.Id));
            }

            return subCategories;
        }

    }
}
