using AttributeService.Application.Dtos.Common;

namespace AttributeService.Application.Dtos
{
    public class AttributeDto : DtoBase
    {
        public string Name { get; set; }
        public ICollection<AttributeValueDto> AttributesValues { get; set; }
    }
}
