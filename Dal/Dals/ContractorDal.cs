using System.Collections.Generic;
using System.Linq;
using Dal.Abstracts;
using Dal.Interfaces;
using Dal.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Models.Entities.Common;
using Models.Entities.Contractors;
using Models.Entities.Homeowners;

namespace Dal
{
    public class ContractorDal : BasicCrudDalAbstract<Contractor>, IContractorDal
    {
        private readonly EntityDbContext _dbContext;
        
        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="dbContext"></param>
        public ContractorDal(EntityDbContext dbContext)
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
        protected override DbSet<Contractor> GetDbSet()
        {
            return _dbContext.Contractors;
        }

        /// <summary>
        /// Include certain fields
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected override IQueryable<Contractor> Interceptor<TQueryable>(
            TQueryable source)
        {
            return  source
                .Include(x => x.UserRef)
                .Include(x => x.HomeownerProjects)
                .ThenInclude(x => x.ProjectPhotos)
                .Include(x => x.ShowcaseProjects)
                .ThenInclude(x => x.ProjectPhotos);
        }
    }
}