using AutoMapper;
using CategoryService.Domain.CategoryAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using Ocdata.Operations.Helpers.ResponseHelper;
using Ocdata.Operations.Repositories.Contracts;

namespace CategoryService.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<string>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCategoryCommandHandler> _logger;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateCategoryCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {

            var entity = await _unitOfWork.Repository<Category>().GetById(request.Id);

            entity.SetCategory(request.ParentId, request.Name, request.DisplayName, request.Description);

            _unitOfWork.Repository<Category>().Update(entity);

            await _unitOfWork.CommitAsync();

            return await Result<string>.SuccessAsync($"{entity.Name} updated successfully.");

        }
    }
}
