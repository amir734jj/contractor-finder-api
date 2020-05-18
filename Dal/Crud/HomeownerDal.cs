using System.Linq;
using Dal.Abstracts;
using Dal.Extensions;
using Dal.Interfaces;
using Dal.Utilities;
using Microsoft.EntityFrameworkCore;
using Models.Entities.Homeowners;

namespace Dal.Crud
{
    public class HomeownerDal : BasicCrudDalAbstract<Homeowner>, IHomeownerDal
    {
        private readonly EntityDbContext _dbContext;
        
        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="dbContext"></param>
        public HomeownerDal(EntityDbContext dbContext)
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
        protected override DbSet<Homeowner> GetDbSet()
        {
            return _dbContext.Homeowners;
        }

        protected override IQueryable<Homeowner> Intercept<TQueryable>(TQueryable queryable)
        {
            return queryable
                .Include(x => x.UserRef)
                .Include(x => x.Projects);
        }

        protected override void UpdateEntity(Homeowner entity, Homeowner dto)
        {
            entity.Address = dto.Address;
            entity.Projects = entity.Projects.IdAwareUpdate(dto.Projects, x => x.Id);
        }
    }
}