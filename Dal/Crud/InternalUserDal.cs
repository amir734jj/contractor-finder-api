using System.Linq;
using Dal.Abstracts;
using Dal.Interfaces;
using Dal.Utilities;
using Microsoft.EntityFrameworkCore;
using Models.Entities.Internals;

namespace Dal.Crud
{
    public class InternalUserDal : BasicCrudDalAbstract<InternalUser>, IInternalUserDal
    {
        private readonly EntityDbContext _dbContext;
        
        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="dbContext"></param>
        public InternalUserDal(EntityDbContext dbContext)
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
        protected override DbSet<InternalUser> GetDbSet()
        {
            return _dbContext.InternalUsers;
        }

        /// <summary>
        /// Include certain fields
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected override IQueryable<InternalUser> Include<TQueryable>(
            TQueryable source)
        {
            return (TQueryable) source
                .Include(x => x.UserRef);
        }
    }
}