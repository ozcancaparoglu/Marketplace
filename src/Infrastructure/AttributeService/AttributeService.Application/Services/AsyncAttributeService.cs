using AttributeService.Application.Cache;
using AttributeService.Application.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ocdata.Operations.Cache;
using Ocdata.Operations.Repositories.Contracts;

namespace AttributeService.Application.Services
{
    public class AsyncAttributeService : IAsyncAttributeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        private List<Domain.AttributeAggregate.Attribute> _allAttributes;
        public List<Domain.AttributeAggregate.Attribute> AllAttributes
        {
            get { return _allAttributes; }
            set { _allAttributes = value; }
        }

        public AsyncAttributeService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _allAttributes = new List<Domain.AttributeAggregate.Attribute>();
        }

        #region Entity Crud

        public async Task<IEnumerable<Domain.AttributeAggregate.Attribute>> List()
        {
            if (!_cacheService.TryGetValue(CacheConstants.AttributeCacheKey, out _allAttributes))
            {
                AllAttributes = (List<Domain.AttributeAggregate.Attribute>)await _unitOfWork.Repository<Domain.AttributeAggregate.Attribute>().GetAll();
                _cacheService.Add(CacheConstants.AttributeCacheKey, AllAttributes, CacheConstants.AttributeCacheTime);
            }

            return AllAttributes;
        }

        public async Task<Domain.AttributeAggregate.Attribute?> Save(AttributeDto dto)
        {
            var existing = await _unitOfWork.Repository<Domain.AttributeAggregate.Attribute>().Find(x => x.Name.ToUpperInvariant() == dto.Name.ToUpperInvariant());

            if (existing != null)
                return null;

            var entity = _mapper.Map<Domain.AttributeAggregate.Attribute>(dto);

            await _unitOfWork.Repository<Domain.AttributeAggregate.Attribute>().Add(entity);

            _cacheService.Remove(CacheConstants.AttributeCacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }

        public async Task<Domain.AttributeAggregate.Attribute?> Update(AttributeDto dto)
        {
            var entity = await _unitOfWork.Repository<Domain.AttributeAggregate.Attribute>().GetById(dto.Id);

            if (entity == null)
                return null;

            entity.SetAttribute(dto.Name, dto.IsRequired, dto.IsVariantable);

            _unitOfWork.Repository<Domain.AttributeAggregate.Attribute>().Update(entity);

            _cacheService.Remove(CacheConstants.AttributeCacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }

        #endregion

        public Domain.AttributeAggregate.Attribute? GetAttributeWithValues(int id)
        {
            var entity = _unitOfWork.Repository<Domain.AttributeAggregate.Attribute>().Table()
                .Include(x => x.AttributesValues)
                .ThenInclude(x => x.Unit)
                .FirstOrDefault(x => x.Id == id);

            if (entity == null)
                return null;

            return entity;
        }
    }
}
