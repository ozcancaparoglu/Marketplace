using AttributeService.Application.Dtos;
using AttributeService.Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ocdata.Operations.Helpers.ResponseHelper;

namespace AttributeService.Application.Features.Attributes.Commands.UpdateAttribute
{
    public class UpdateAttributeCommandHandler : IRequestHandler<UpdateAttributeCommand, Result<string>>
    {
        private readonly IAsyncAttributeService _attributeService;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAttributeCommandHandler> _logger;

        public UpdateAttributeCommandHandler(IAsyncAttributeService attributeService, IMapper mapper, ILogger<UpdateAttributeCommandHandler> logger)
        {
            _attributeService = attributeService ?? throw new ArgumentNullException(nameof(attributeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<string>> Handle(UpdateAttributeCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<AttributeDto>(request);

            var entity = await _attributeService.Update(dto);

            if (entity == null)
                return await Result<string>.FailureAsync($"Db does not contains record: {request.Id}-{request.Name}");

            _logger.LogInformation(message: $"Attribute {entity.Id}-{entity.Name} updated successfully.");

            return await Result<string>.SuccessAsync($"{entity.Id}-{entity.Name} updated successfully.");

            throw new NotImplementedException();
        }
    }
}
