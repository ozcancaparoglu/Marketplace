using AutoMapper;
using CategoryService.Application.Dtos.Common;
using Ocdata.Operations.Cache;
using Ocdata.Operations.Entities;
using Ocdata.Operations.Repositories.Contracts;
using System.Linq.Expressions;

namespace CategoryService.Application.Services.Common
{
    public abstract class CrudBaseService<TEntity> where TEntity : EntityBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IAsyncRepository<TEntity> _repo;

        private List<TEntity> _allEntities;
        public List<TEntity> AllEntities
        {
            get { return _allEntities; }
            set { AllEntities = value; }
        }

        protected CrudBaseService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _allEntities = new List<TEntity>();
            _repo = _unitOfWork.Repository<TEntity>();
        }

        public async Task<TEntity?> Save<TDto>(TDto dto, Expression<Func<TEntity, bool>> filter, string cacheKey) where TDto : DtoBase
        {
            var existing = await _unitOfWork.Repository<TEntity>().Find(filter);

            if (existing != null)
                return null;

            var entity = _mapper.Map<TEntity>(dto);

            await _repo.Add(entity);

            _cacheService.Remove(cacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }

        public async Task<TEntity?> Update<TDto>(TDto dto, Expression<Func<TEntity, bool>> filter, string cacheKey) where TDto : DtoBase
        {
            var entity = await _unitOfWork.Repository<TEntity>().GetById(dto.Id);

            if (entity == null)
                return null;

            //Şimdi sıçtık :)
            //entity.SetCategory(dto.ParentId, dto.Name, dto.DisplayName, dto.Description);

            _repo.Update(entity);

            _cacheService.Remove(cacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }


    }
}
