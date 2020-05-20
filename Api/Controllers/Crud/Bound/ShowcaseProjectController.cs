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
    [RoleAuthorize(RoleEnum.Contractor)]
    [Route("Api/[controller]")]
    public class ShowcaseProjectController : BasicCrudBoundController<ShowcaseProject>
    {
        private readonly IShowcaseProjectBoundLogic _showcaseProjectBoundLogic;

        private readonly UserManager<User> _userManager;

        public ShowcaseProjectController(IShowcaseProjectBoundLogic showcaseProjectBoundLogic, UserManager<User> userManager)
        {
            _showcaseProjectBoundLogic = showcaseProjectBoundLogic;
            _userManager = userManager;
        }

        protected override IBasicCrudBoundLogic<ShowcaseProject> BasicCrudUserBoundLogic()
        {
            return _showcaseProjectBoundLogic;
        }

        protected override UserManager<User> UserManager()
        {
            return _userManager;
        }
    }
}