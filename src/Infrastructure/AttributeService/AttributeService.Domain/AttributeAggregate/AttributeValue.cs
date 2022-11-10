using Ocdata.Operations.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttributeService.Domain.AttributeAggregate
{
    public class AttributeValue : EntityBase
    {
        public int? AttributeId { get; private set; }      
        [ForeignKey("AttributeId")]
        public Attribute Attribute { get; private set; }
        
        public string Value { get; private set; }

        public int? UnitId { get; private set; }
        [ForeignKey("UnitId")]
        public Unit Unit { get; private set; }

        protected AttributeValue()
        {
        }

        public AttributeValue(int? attributeId, string value, int? unitId)
        {
            AttributeId = attributeId;
            Value = value;
            UnitId = unitId;
        }

        public void SetAttributeValue(int? attributeId, string value, int? unitId)
        {
            AttributeId = attributeId;
            Value = value;
            UnitId = unitId;
        }

        public void SetValue(string value)
        {
            Value = value;
        }

        public void SetUnit(int? unitId)
        {
            UnitId = unitId;
        }
    }
}
