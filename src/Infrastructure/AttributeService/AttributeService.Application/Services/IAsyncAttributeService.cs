using AttributeService.Application.Dtos;

namespace AttributeService.Application.Services
{
    public interface IAsyncAttributeService
    {
        Task<IEnumerable<Domain.AttributeAggregate.Attribute>> List();
        Task<Domain.AttributeAggregate.Attribute?> Save(AttributeDto dto);
        Task<Domain.AttributeAggregate.Attribute?> Update(AttributeDto dto);
    }
}