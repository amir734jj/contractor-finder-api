using System.Collections.Generic;
using Logic.Abstracts;
using Logic.Interfaces;
using Logic.Interfaces.Basic;
using Logic.Interfaces.Bound;
using Models.Entities.Projects;
using Models.Entities.Users;

namespace Logic.Crud.Bound
{
    public class ProjectMilestoneBoundLogic : BasicCrudBoundLogicAbstract<ProjectMilestone>, IProjectMilestoneBoundLogic
    {
        private readonly IUserLogic _userLogic;

        public ProjectMilestoneBoundLogic(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        protected override List<ProjectMilestone> ResolveSource(User user)
        {
            return user.ContractorRef.ProjectMilestones;
        }

        protected override IBasicCrudLogic<User> UserLogic()
        {
            return _userLogic;
        }
    }
}