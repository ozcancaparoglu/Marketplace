using AttributeService.Application.Dtos;
using AttributeService.Domain.AttributeAggregate;

namespace AttributeService.Application.Services
{
    public interface IAsyncAttributeValueService
    {
        Task<IEnumerable<AttributeValue>> List();
        Task<AttributeValue?> Save(AttributeValueDto dto);
        Task<AttributeValue?> Update(AttributeValueDto dto);
    }
}