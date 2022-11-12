using MediatR;
using Ocdata.Operations.Helpers.ResponseHelper;

namespace AttributeService.Application.Features.Attributes.Commands.SaveAttribute
{
    public class SaveAttributeCommand : IRequest<Result<string>>
    {
        public string Name { get; set; }
    }
}
