using AutoMapper;
using CategoryService.Application.Dtos;
using CategoryService.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Ocdata.Operations.Helpers.ResponseHelper;

namespace CategoryService.Application.Features.Categories.Commands.SaveCategory
{
    public class SaveCategoryCommandHandler : IRequestHandler<SaveCategoryCommand, Result<string>>
    {
        private readonly IAsyncCategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<SaveCategoryCommandHandler> _logger;

        public SaveCategoryCommandHandler(IAsyncCategoryService categoryService, IMapper mapper, ILogger<SaveCategoryCommandHandler> logger)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<string>> Handle(SaveCategoryCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CategoryDto>(request);

            var entity = await _categoryService.Save(dto);

            if(entity == null)
                return await Result<string>.FailureAsync("Category already exists.");

            _logger.LogInformation(message: $"Category {entity.Name}, {entity.DisplayName} is successfully created.");

            return await Result<string>.SuccessAsync($"Category {entity.Name}, {entity.DisplayName} is successfully created.");
        }
    }
}
