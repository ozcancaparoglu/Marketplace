using CategoryService.Application.Dtos.Common;

namespace CategoryService.Application.Dtos
{
    public class CategoryDto : DtoBase
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}
