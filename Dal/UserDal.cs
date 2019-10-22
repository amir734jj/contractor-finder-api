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
    public class UserDal :  BasicCrudDalAbstract<User>, IUserDal
    {
        private readonly EntityDbContext _dbContext;
        
        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="dbContext"></param>
        public UserDal(EntityDbContext dbContext)
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
        protected override DbSet<User> GetDbSet()
        {
            return _dbContext.Users;
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await GetDbSet()
                .OrderBy(x => x.Fullname)
                .ToListAsync();
        }
        
        public override async Task<User> Update(int id, User dto)
        {
            var entity = await Get(id);

            entity.Fullname = dto.Fullname;
            entity.Password = dto.Password;
            entity.Username = dto.Username;
            entity.UserRoleEnum = dto.UserRoleEnum;
            entity.Phone = dto.Phone;
            entity.Email = dto.Email;
            
            return await base.Update(id, entity);
        }
    }
}