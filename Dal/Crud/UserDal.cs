using System.Linq;
using Dal.Abstracts;
using Dal.Interfaces;
using Dal.Utilities;
using Microsoft.EntityFrameworkCore;
using Models.Entities.Users;

namespace Dal.Crud
{
    public class UserDal : BasicCrudDalAbstract<User>, IUserDal
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

        protected override IQueryable<User> Intercept<TQueryable>(TQueryable queryable)
        {
            return queryable
                .Include(x => x.PhotoRef)
                .Include(x => x.ContractorRef)
                .Include(x => x.HomeownerRef)
                .Include(x => x.InternalUserRef);
        }

        protected override void UpdateEntity(User entity, User dto)
        {
            entity.Description = dto.Description;
            entity.Name = dto.Name;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.PhotoKey = dto.PhotoKey;
        }
    }
}