using AttributeService.Application.Dtos.Common;

namespace AttributeService.Application.Dtos
{
    public class AttributeDto : DtoBase
    {
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool IsVariantable { get; set; }
        public ICollection<AttributeValueDto> AttributesValues { get; set; }
    }
}
