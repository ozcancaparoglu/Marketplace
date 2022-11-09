using CategoryService.Application.Dtos;
using CategoryService.Domain.CategoryAggregate;

namespace CategoryService.Application.Services
{
    public interface IAsyncCategoryAttributeService
    {
        Task<CategoryAttribute?> Save(CategoryAttributeDto dto);
    }
}