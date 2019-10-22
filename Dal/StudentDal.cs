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
    public class StudentDal : BasicCrudDalAbstract<Student>, IStudentDal
    {
        private readonly EntityDbContext _dbContext;
        
        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="dbContext"></param>
        public StudentDal(EntityDbContext dbContext)
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
        protected override DbSet<Student> GetDbSet()
        {
            return _dbContext.Students;
        }

        /// <summary>
        /// Override to include related entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<Student> Get(int id)
        {
            return await GetDbSet()
                .Include(x => x.Driver)
                .Include(x => x.Driver.Host)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Override to include related entity
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<Student>> GetAll()
        {
            return await GetDbSet()
                .Include(x => x.Driver)
                .Include(x => x.Driver.Host)
                .OrderBy(x => x.Fullname)
                .ToListAsync();
        }

        public override async Task<Student> Update(int id, Student dto)
        {
            var entity = await Get(id);

            entity.DisplayId = dto.DisplayId;
            entity.Fullname = dto.Fullname;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;
            entity.Major = dto.Major;
            entity.Country = dto.Country;
            entity.University = dto.University;
            entity.IsFamily = dto.IsFamily;
            entity.Interests = dto.Interests;
            entity.NeedCarSeat = dto.NeedCarSeat;
            entity.KosherFood = dto.KosherFood;
            entity.FamilySize = dto.FamilySize;
            
            return await base.Update(id, entity);
        }
    }
}