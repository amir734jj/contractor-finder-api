using System.Collections.Generic;
using System.Linq;
using Dal.Abstracts;
using Dal.Interfaces;
using Dal.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Models.Entities.Common;
using Models.Entities.Homeowners;
using Models.Entities.Internals;

namespace Dal
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
        protected override IQueryable<InternalUser> Interceptor<TQueryable>(
            TQueryable source)
        {
            return (TQueryable) source
                .Include(x => x.UserRef);
        }
    }
}