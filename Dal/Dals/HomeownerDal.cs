using System.Collections.Generic;
using System.Linq;
using Dal.Abstracts;
using Dal.Interfaces;
using Dal.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Models.Entities.Common;
using Models.Entities.Homeowners;

namespace Dal
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

        /// <summary>
        /// Include certain fields
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected override IQueryable<Homeowner> Interceptor<TQueryable>(
            TQueryable source)
        {
            return source
                .Include(x => x.UserRef)
                .Include(x => x.Projects);
        }
    }
}