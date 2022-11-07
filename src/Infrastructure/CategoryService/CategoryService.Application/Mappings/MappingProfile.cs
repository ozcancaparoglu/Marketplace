﻿using AutoMapper;
using CategoryService.Application.ApiContracts.Queries;
using CategoryService.Application.Features.Categories.Commands.SaveCategory;
using CategoryService.Domain.CategoryAggregate;

namespace CategoryService.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, SaveCategoryCommand>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
        }
    }
}