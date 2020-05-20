using System.Linq;
using Dal.Abstracts;
using Dal.Extensions;
using Dal.Interfaces;
using Dal.Utilities;
using Microsoft.EntityFrameworkCore;
using Models.Entities.Contractors;

namespace Dal.Crud
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

        protected override IQueryable<Contractor> Intercept<TQueryable>(TQueryable queryable)
        {
            return queryable
                .Include(x => x.UserRef);
        }

        protected override void UpdateEntity(Contractor entity, Contractor dto)
        {
            entity.Speciality = entity.Speciality.IdAwareUpdate(dto.Speciality, x => x.Id);
            entity.ProjectMilestones = entity.ProjectMilestones.IdAwareUpdate(dto.ProjectMilestones, x => x.Id);
            entity.ShowcaseProjects = entity.ShowcaseProjects.IdAwareUpdate(dto.ShowcaseProjects, x => x.Id);
        }
    }
}