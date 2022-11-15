using AttributeService.Application.ApiContracts.Queries;
using AttributeService.Application.Services;
using AutoMapper;
using MediatR;
using Ocdata.Operations.Helpers.ResponseHelper;

namespace AttributeService.Application.Features.Attributes.Queries.ListAttributes
{
    public class ListAttributesQueryHandler : IRequestHandler<ListAttributesQuery, Result<List<AttributeResponse>>>
    {
        private readonly IAsyncAttributeService _attributeService;
        private readonly IMapper _mapper;

        public ListAttributesQueryHandler(IAsyncAttributeService attributeService, IMapper mapper)
        {
            _attributeService = attributeService ?? throw new ArgumentNullException(nameof(attributeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<List<AttributeResponse>>> Handle(ListAttributesQuery request, CancellationToken cancellationToken)
        {
            return await Result<List<AttributeResponse>>.SuccessAsync(_mapper.Map<List<AttributeResponse>>(await _attributeService.List()));
        }
    }
}
