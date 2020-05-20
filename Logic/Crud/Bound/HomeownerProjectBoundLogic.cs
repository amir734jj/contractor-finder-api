using System.Collections.Generic;
using Logic.Abstracts;
using Logic.Interfaces;
using Logic.Interfaces.Basic;
using Logic.Interfaces.Bound;
using Models.Entities.Homeowners;
using Models.Entities.Projects;
using Models.Entities.Users;

namespace Logic.Crud.Bound
{
    public class HomeownerProjectBoundLogic : BasicCrudBoundLogicAbstract<Homeowner, HomeownerProject>, IHomeownerProjectBoundLogic
    {
        private readonly IUserLogic _userLogic;
        
        private readonly IHomeownerLogic _homeownerLogic;

        public HomeownerProjectBoundLogic(IUserLogic userLogic, IHomeownerLogic homeownerLogic)
        {
            _userLogic = userLogic;
            _homeownerLogic = homeownerLogic;
        }

        protected override (Homeowner, List<HomeownerProject>) ResolveSources(User user)
        {
            return (user.HomeownerRef, user.HomeownerRef.Projects);
        }

        protected override IBasicCrudLogic<User> UserLogic()
        {
            return _userLogic;
        }

        protected override IBasicCrudLogic<Homeowner> ParentLogic()
        {
            return _homeownerLogic;
        }
    }
}