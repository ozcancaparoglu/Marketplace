using AutoMapper;
using CategoryService.Domain.CategoryAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using Ocdata.Operations.Helpers.ResponseHelper;
using Ocdata.Operations.Repositories.Contracts;

namespace CategoryService.Application.Features.Categories.Commands.SaveCategory
{
    public class SaveCategoryCommandHandler : IRequestHandler<SaveCategoryCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SaveCategoryCommandHandler> _logger;

        public SaveCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SaveCategoryCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<string>> Handle(SaveCategoryCommand request, CancellationToken cancellationToken)
        {
            var existing = await _unitOfWork.Repository<Category>().Find(x => x.Name == request.Name
            && x.DisplayName == request.DisplayName);

            if (existing != null)
                return await Result<string>.FailureAsync("Category already exists.");

            var entity = _mapper.Map<Category>(request);
            await _unitOfWork.Repository<Category>().Add(entity);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation(message: $"Category {entity.Name}, {entity.DisplayName} is successfully created.");

            return await Result<string>.SuccessAsync($"Category {entity.Name}, {entity.DisplayName} is successfully created.");
        }
    }
}
