using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Abstracts;
using DAL.Interfaces;
using DAL.Utilities;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL
{
    public class DriverDal : BasicCrudDalAbstract<Driver>, IDriverDal
    {
        private readonly EntityDbContext _dbContext;
        
        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="dbContext"></param>
        public DriverDal(EntityDbContext dbContext)
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
        protected override DbSet<Driver> GetDbSet()
        {
            return _dbContext.Drivers;
        }

        /// <summary>
        /// Override to include related entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<Driver> Get(int id)
        {
            return await GetDbSet().Include(x => x.Host).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Override to include related entity
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<Driver>> GetAll()
        {
            return await GetDbSet()
                .Include(x => x.Host)
                .Include(x => x.Host.Drivers)
                .Include(x => x.Students)
                .OrderBy(x => x.Fullname)
                .ToListAsync();
        }

        public override async Task<Driver> Update(int id, Driver dto)
        {
            var entity = await Get(id);

            entity.Fullname = dto.Fullname;
            entity.Role = dto.Role;
            entity.Phone = dto.Phone;
            entity.Email = dto.Email;
            entity.Capacity = dto.Capacity;
            entity.Navigator = dto.Navigator;
            entity.DisplayId = dto.DisplayId;
            entity.RequireNavigator = dto.RequireNavigator;
            entity.HaveChildSeat = dto.HaveChildSeat;
            
            return await base.Update(id, entity);
        }
    }
}