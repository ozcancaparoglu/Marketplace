using Ocdata.Operations.Entities;

namespace AttributeService.Domain.AttributeAggregate
{
    public class Unit : EntityBase
    {
        public string Name { get; private set; }

        protected Unit()
        {
        }

        public Unit(string name)
        {
            Name = name;
        }

        public void SetUnit(string name)
        {
            Name = name;
        }
    }
}
