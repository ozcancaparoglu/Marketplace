using AttributeService.Application.Cache;
using AttributeService.Application.Dtos;
using AttributeService.Domain.AttributeAggregate;
using AutoMapper;
using Ocdata.Operations.Cache;
using Ocdata.Operations.Repositories.Contracts;

namespace AttributeService.Application.Services
{
    public class AsyncUnitService : IAsyncUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        private List<Unit> _allUnits;
        public List<Unit> AllUnits
        {
            get { return _allUnits; }
            set { _allUnits = value; }
        }

        public AsyncUnitService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _allUnits = new List<Unit>();
        }

        public async Task<IEnumerable<Unit>> List()
        {
            if (!_cacheService.TryGetValue(CacheConstants.UnitCacheKey, out _allUnits))
            {
                AllUnits = (List<Unit>)await _unitOfWork.Repository<Unit>().GetAll();
                _cacheService.Add(CacheConstants.UnitCacheKey, AllUnits, CacheConstants.UnitCacheTime);
            }

            return AllUnits;
        }

        public async Task<Unit?> Save(UnitDto dto)
        {
            var existing = await _unitOfWork.Repository<Unit>().Find(x => x.Name.ToUpperInvariant() == dto.Name.ToUpperInvariant());

            if (existing != null)
                return null;

            var entity = _mapper.Map<Unit>(dto);

            await _unitOfWork.Repository<Unit>().Add(entity);

            _cacheService.Remove(CacheConstants.UnitCacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }

        public async Task<Unit?> Update(UnitDto dto)
        {
            var entity = await _unitOfWork.Repository<Unit>().GetById(dto.Id);

            if (entity == null)
                return null;

            entity.SetUnit(dto.Name);

            _unitOfWork.Repository<Unit>().Update(entity);

            _cacheService.Remove(CacheConstants.UnitCacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }
    }
}
