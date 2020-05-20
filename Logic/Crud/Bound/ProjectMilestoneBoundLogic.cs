using System.Collections.Generic;
using Logic.Abstracts;
using Logic.Interfaces;
using Logic.Interfaces.Basic;
using Logic.Interfaces.Bound;
using Models.Entities.Contractors;
using Models.Entities.Projects;
using Models.Entities.Users;

namespace Logic.Crud.Bound
{
    public class ProjectMilestoneBoundLogic : BasicCrudBoundLogicAbstract<Contractor, ProjectMilestone>, IProjectMilestoneBoundLogic
    {
        private readonly IUserLogic _userLogic;
        
        private readonly IContractorLogic _contractorLogic;

        public ProjectMilestoneBoundLogic(IUserLogic userLogic, IContractorLogic contractorLogic)
        {
            _userLogic = userLogic;
            _contractorLogic = contractorLogic;
        }

        protected override (Contractor, List<ProjectMilestone>) ResolveSources(User user)
        {
            return (user.ContractorRef, user.ContractorRef.ProjectMilestones);
        }

        protected override IBasicCrudLogic<User> UserLogic()
        {
            return _userLogic;
        }

        protected override IBasicCrudLogic<Contractor> ParentLogic()
        {
            return _contractorLogic;
        }
    }
}