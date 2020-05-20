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
    public class ShowcaseProjectBoundLogic : BasicCrudBoundLogicAbstract<Contractor, ShowcaseProject>, IShowcaseProjectBoundLogic
    {
        private readonly IUserLogic _userLogic;
        
        private readonly IContractorLogic _contractorLogic;

        public ShowcaseProjectBoundLogic(IUserLogic userLogic, IContractorLogic contractorLogic)
        {
            _userLogic = userLogic;
            _contractorLogic = contractorLogic;
        }

        protected override (Contractor, List<ShowcaseProject>) ResolveSources(User user)
        {
            return (user.ContractorRef, user.ContractorRef.ShowcaseProjects);
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