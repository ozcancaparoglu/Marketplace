using AttributeService.Application.Cache;
using AttributeService.Application.Dtos;
using AttributeService.Domain.AttributeAggregate;
using AutoMapper;
using Ocdata.Operations.Cache;
using Ocdata.Operations.Repositories.Contracts;

namespace AttributeService.Application.Services
{
    public class AsyncAttributeValueService : IAsyncAttributeValueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        private List<AttributeValue> _allAttributeValues;
        public List<AttributeValue> AllAttributeValues
        {
            get { return _allAttributeValues; }
            set { _allAttributeValues = value; }
        }

        public AsyncAttributeValueService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _allAttributeValues = new List<AttributeValue>();
        }

        public async Task<IEnumerable<AttributeValue>> List()
        {
            if (!_cacheService.TryGetValue(CacheConstants.AttributeValueCacheKey, out _allAttributeValues))
            {
                AllAttributeValues = (List<AttributeValue>)await _unitOfWork.Repository<AttributeValue>().GetAll();
                _cacheService.Add(CacheConstants.AttributeValueCacheKey, AllAttributeValues, CacheConstants.AttributeValueCacheTime);
            }

            return AllAttributeValues;
        }

        public async Task<AttributeValue?> Save(AttributeValueDto dto)
        {
            var existing = await _unitOfWork.Repository<AttributeValue>().Find(x => x.AttributeId == dto.AttributeId 
            && x.Value.ToUpperInvariant() == dto.Value.ToUpperInvariant());

            if (existing != null)
                return null;

            var entity = _mapper.Map<AttributeValue>(dto);

            await _unitOfWork.Repository<AttributeValue>().Add(entity);

            _cacheService.Remove(CacheConstants.AttributeValueCacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }

        public async Task<AttributeValue?> Update(AttributeValueDto dto)
        {
            var entity = await _unitOfWork.Repository<AttributeValue>().GetById(dto.Id);

            if (entity == null)
                return null;

            entity.SetAttributeValue(dto.AttributeId, dto.Value, dto.UnitId);

            _unitOfWork.Repository<AttributeValue>().Update(entity);

            _cacheService.Remove(CacheConstants.AttributeValueCacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }
    }
}
