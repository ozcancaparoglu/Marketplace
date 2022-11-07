namespace CategoryService.Application.ApiContracts.Queries
{
    public class CategoryResponse
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}
