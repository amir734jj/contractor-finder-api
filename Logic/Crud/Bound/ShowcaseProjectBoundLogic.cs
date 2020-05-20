using System.Collections.Generic;
using Logic.Abstracts;
using Logic.Interfaces;
using Logic.Interfaces.Basic;
using Logic.Interfaces.Bound;
using Models.Entities.Projects;
using Models.Entities.Users;

namespace Logic.Crud.Bound
{
    public class ShowcaseProjectBoundLogic : BasicCrudBoundLogicAbstract<ShowcaseProject>, IShowcaseProjectBoundLogic
    {
        private readonly IUserLogic _userLogic;

        public ShowcaseProjectBoundLogic(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        protected override List<ShowcaseProject> ResolveSource(User user)
        {
            return user.ContractorRef.ShowcaseProjects;
        }

        protected override IBasicCrudLogic<User> UserLogic()
        {
            return _userLogic;
        }
    }
}