using Newtonsoft.Json;
using Ocdata.Operations.Entities;
using System.ComponentModel.DataAnnotations;

namespace AttributeService.Domain.AttributeAggregate
{
    public class Attribute : EntityBase
    {
        [Required]
        [StringLength(250)]
        public string Name { get; private set; }

        private readonly List<AttributeValue> _attributesValues;
        public IReadOnlyCollection<AttributeValue> AttributesValues => _attributesValues;

        protected Attribute()
        {
            _attributesValues = new List<AttributeValue>();
        }

        public Attribute(string name) : this()
        {
            Name = name;
        }

        public void SetAttribute(string name)
        {
            Name = name;
        }

        public void VerifyOrAddAttributesValues(string value, int unitId)
        {
            if (!_attributesValues.Any(x => x.Value == value && x.Unit.Id == unitId))
            {
                var newEntry = new AttributeValue(Id, value, unitId);

                _attributesValues.Add(newEntry);
            }
            else
            {
                var existing = _attributesValues.FirstOrDefault(x => x.Value == value && x.UnitId == unitId);
                if (existing != null)
                    existing.SetAttributeValue(Id, value, unitId);
            }
        }
    }
}
