using AttributeService.Application.Dtos.Common;

namespace AttributeService.Application.Dtos
{
    public class AttributeValueDto : DtoBase
    {
        public int? AttributeId { get; set; }
        public string Value { get; set; }
        public int? UnitId { get; set; }
    }
}