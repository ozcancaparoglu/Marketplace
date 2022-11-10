using AttributeService.Application.Dtos;
using AttributeService.Domain.AttributeAggregate;
using AutoMapper;

namespace AttributeService.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Features  
            #endregion

            #region ApiContracts
            #endregion

            #region Entity<>Dto
            CreateMap<Domain.AttributeAggregate.Attribute, AttributeDto>().ReverseMap();
            CreateMap<AttributeValue, AttributeValueDto>().ReverseMap();
            CreateMap<Unit, UnitDto>().ReverseMap();
            #endregion
        }
    }
}
