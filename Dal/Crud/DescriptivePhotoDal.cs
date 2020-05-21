using System.Linq;
using Dal.Abstracts;
using Dal.Interfaces;
using Dal.Utilities;
using Microsoft.EntityFrameworkCore;
using Models.Entities.Common;

namespace Dal.Crud
{
    public class DescriptivePhotoDal : BasicCrudDalAbstract<DescriptivePhoto>, IDescriptivePhotoDal
    {
        private readonly EntityDbContext _dbContext;

        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="dbContext"></param>
        public DescriptivePhotoDal(EntityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Returns database context
        /// </summary>
        /// <returns></returns>
        protected override DbContext GetDbContext()
        {
            return _dbContext;
        }

        /// <summary>
        /// Returns students entity
        /// </summary>
        /// <returns></returns>
        protected override DbSet<DescriptivePhoto> GetDbSet()
        {
            return _dbContext.DescriptivePhotos;
        }

        protected override IQueryable<DescriptivePhoto> Intercept<TQueryable>(TQueryable queryable)
        {
            return queryable;
        }

        protected override void UpdateEntity(DescriptivePhoto entity, DescriptivePhoto dto)
        {
            entity.Key = dto.Key;
        }
    }
}