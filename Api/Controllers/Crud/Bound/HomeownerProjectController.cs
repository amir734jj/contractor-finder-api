using Api.Abstracts;
using Api.Attributes;
using Logic.Interfaces;
using Logic.Interfaces.Bound;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Projects;
using Models.Entities.Users;
using Models.Enums;

namespace Api.Controllers.Crud.Bound
{
    [RoleAuthorize(RoleEnum.Homeowner)]
    [Route("Api/[controller]")]
    public class HomeownerProjectController : BasicCrudBoundController<HomeownerProject>
    {
        private readonly IHomeownerProjectBoundLogic _homeownerProjectBoundLogic;

        private readonly UserManager<User> _userManager;

        public HomeownerProjectController(IHomeownerProjectBoundLogic homeownerProjectBoundLogic, UserManager<User> userManager)
        {
            _homeownerProjectBoundLogic = homeownerProjectBoundLogic;
            _userManager = userManager;
        }

        protected override IBasicCrudBoundLogic<HomeownerProject> BasicCrudUserBoundLogic()
        {
            return _homeownerProjectBoundLogic;
        }

        protected override UserManager<User> UserManager()
        {
            return _userManager;
        }
    }
}