using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dal.Abstracts;
using Dal.Interfaces;
using Dal.Utilities;
using Microsoft.EntityFrameworkCore;
using Models.Entities.ContractorEntities;

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
        /// Override to include related entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<Contractor> Get(Guid id)
        {
            return await GetDbSet()
                .Include(x => x.ProfilePhoto)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Override to include related entity
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<Contractor>> GetAll()
        {
            return await GetDbSet()
                .Include(x => x.ProfilePhoto)
                .ToListAsync();
        }
    }
}