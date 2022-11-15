using MediatR;
using Ocdata.Operations.Helpers.ResponseHelper;

namespace AttributeService.Application.Features.Attributes.Commands.UpdateAttribute
{
    public class UpdateAttributeCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
