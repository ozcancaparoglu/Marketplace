using AttributeService.Application.ApiContracts.Queries;
using AttributeService.Application.Dtos;
using AttributeService.Application.Features.Attributes.Commands.SaveAttribute;
using AttributeService.Application.Features.Attributes.Commands.UpdateAttribute;
using AttributeService.Domain.AttributeAggregate;
using AutoMapper;
using Attribute = AttributeService.Domain.AttributeAggregate.Attribute;

namespace AttributeService.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Features
            CreateMap<SaveAttributeCommand, AttributeDto>().ReverseMap();
            CreateMap<UpdateAttributeCommand, AttributeDto>().ReverseMap();
            #endregion

            #region ApiContracts
            CreateMap<Attribute, AttributeResponse>().ReverseMap();
            #endregion

            #region Entity<>Dto
            CreateMap<Attribute, AttributeDto>().ReverseMap();
            CreateMap<AttributeValue, AttributeValueDto>().ReverseMap();
            CreateMap<Unit, UnitDto>().ReverseMap();
            #endregion
        }
    }
}
