using CategoryService.Application.Dtos;
using CategoryService.Domain.CategoryAggregate;

namespace CategoryService.Application.Services
{
    public interface IAsyncCategoryService
    {
        Task<IEnumerable<Category>> ChildCategories(int parentId);
        Task<IEnumerable<Category>> List();
        Task<Category?> Save(CategoryDto dto);
        Task<Category?> Update(CategoryDto dto);
    }
}