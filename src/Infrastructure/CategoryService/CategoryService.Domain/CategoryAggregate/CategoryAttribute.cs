using Ocdata.Operations.Entities;

namespace CategoryService.Domain.CategoryAggregate
{
    public class CategoryAttribute : EntityBase
    {
        public int CategoryId { get; protected set; }
        public int AttributeId { get; protected set; }
        public bool IsRequired { get; protected set; }
        public bool IsVariantable { get; protected set; }

        protected CategoryAttribute()
        {
        }

        public CategoryAttribute(int categoryId, int attributeId, bool isRequired, bool isVariantable) : this()
        {
            CategoryId = categoryId;
            AttributeId = attributeId;
            IsRequired = isRequired;
            IsVariantable = isVariantable;
        }

        public void SetCategoryAttribute(int categoryId, int attributeId, bool isRequired, bool isVariantable)
        {
            CategoryId = categoryId;
            AttributeId = attributeId;
            IsRequired = isRequired;
            IsVariantable = isVariantable;
        }


    }
}
