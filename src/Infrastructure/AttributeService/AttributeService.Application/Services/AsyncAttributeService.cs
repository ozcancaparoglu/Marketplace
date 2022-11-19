using AttributeService.Application.Cache;
using AttributeService.Application.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ocdata.Operations.Cache;
using Ocdata.Operations.Repositories.Contracts;
using Attribute = AttributeService.Domain.AttributeAggregate.Attribute;

namespace AttributeService.Application.Services
{
    public class AsyncAttributeService : IAsyncAttributeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        private List<Attribute> _allAttributes;
        public List<Attribute> AllAttributes
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

        public async Task<IEnumerable<Attribute>> List()
        {
            if (!_cacheService.TryGetValue(CacheConstants.AttributeCacheKey, out _allAttributes))
            {
                AllAttributes = (List<Attribute>)await _unitOfWork.Repository<Attribute>().GetAll();
                _cacheService.Add(CacheConstants.AttributeCacheKey, AllAttributes, CacheConstants.AttributeCacheTime);
            }

            return AllAttributes;
        }

        public async Task<Attribute?> Save(AttributeDto dto)
        {
            var existing = await _unitOfWork.Repository<Attribute>().Find(x => x.Name == dto.Name);

            if (existing != null)
                return null;

            var entity = _mapper.Map<Attribute>(dto);

            await _unitOfWork.Repository<Attribute>().Add(entity);

            _cacheService.Remove(CacheConstants.AttributeCacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }

        public async Task<Attribute?> Update(AttributeDto dto)
        {
            var entity = await _unitOfWork.Repository<Attribute>().GetById(dto.Id);

            if (entity == null)
                return null;

            entity.SetAttribute(dto.Name);

            _unitOfWork.Repository<Attribute>().Update(entity);

            _cacheService.Remove(CacheConstants.AttributeCacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }

        #endregion

        public Attribute? GetAttributeWithValues(int id)
        {
            var entity = _unitOfWork.Repository<Attribute>().Table()
                .Include(x => x.AttributesValues)
                .ThenInclude(x => x.Unit)
                .FirstOrDefault(x => x.Id == id);

            if (entity == null)
                return null;

            return entity;
        }
    }
}
