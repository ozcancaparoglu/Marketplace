using AttributeService.Application.Dtos;
using Attribute = AttributeService.Domain.AttributeAggregate.Attribute;

namespace AttributeService.Application.Services
{
    public interface IAsyncAttributeService
    {
        Task<IEnumerable<Attribute>> List();
        Task<Attribute?> Save(AttributeDto dto);
        Task<Attribute?> Update(AttributeDto dto);
    }
}