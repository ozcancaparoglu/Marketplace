using AutoMapper;
using CategoryService.Application.ApiContracts.Queries;
using CategoryService.Application.Dtos;
using CategoryService.Application.Features.Categories.Commands.SaveCategory;
using CategoryService.Application.Features.Categories.Commands.UpdateCategory;
using CategoryService.Domain.CategoryAggregate;

namespace CategoryService.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Features
            CreateMap<CategoryDto, SaveCategoryCommand>().ReverseMap();
            CreateMap<CategoryDto, UpdateCategoryCommand>().ReverseMap();
            #endregion

            #region ApiContracts
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Category, CategoryTreeResponse>().MaxDepth(5).ReverseMap();
            #endregion

            #region Entity<>Dto
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryAttribute, CategoryDto>().ReverseMap();
            #endregion
        }
    }
}
