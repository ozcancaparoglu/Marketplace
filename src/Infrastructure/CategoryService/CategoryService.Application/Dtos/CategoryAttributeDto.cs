using CategoryService.Application.Dtos.Common;

namespace CategoryService.Application.Dtos
{
    public class CategoryAttributeDto : DtoBase
    {
        public int CategoryId { get; set; }
        public int AttributeId { get; set; }
        public bool IsRequired { get; set; }
        public bool IsVariantable { get; set; }
    }
}
