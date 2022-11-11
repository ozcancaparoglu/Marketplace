using AttributeService.Application.Dtos;
using AttributeService.Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ocdata.Operations.Helpers.ResponseHelper;

namespace AttributeService.Application.Features.Attributes.Commands.SaveAttribute
{
    public class SaveAttributeCommandHandler : IRequestHandler<SaveAttributeCommand, Result<string>>
    {
        private readonly IAsyncAttributeService _attributeService;
        private readonly IMapper _mapper;
        private readonly ILogger<SaveAttributeCommandHandler> _logger;

        public SaveAttributeCommandHandler(IAsyncAttributeService attributeService, IMapper mapper, ILogger<SaveAttributeCommandHandler> logger)
        {
            _attributeService = attributeService ?? throw new ArgumentNullException(nameof(attributeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<string>> Handle(SaveAttributeCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<AttributeDto>(request);

            var entity = await _attributeService.Save(dto);

            if (entity == null)
                return await Result<string>.FailureAsync("Attribute already exists.");

            _logger.LogInformation(message: $"Attribute {entity.Name} is successfully created.");

            return await Result<string>.SuccessAsync($"Attribute {entity.Name} is successfully created.");
        }
    }
}
