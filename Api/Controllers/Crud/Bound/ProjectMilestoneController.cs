using Api.Abstracts;
using Logic.Interfaces;
using Logic.Interfaces.Bound;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Projects;
using Models.Entities.Users;

namespace Api.Controllers.Crud.Bound
{
    [Authorize]
    [Route("Api/[controller]")]
    public class ProjectMilestoneBoundController : BasicCrudBoundController<ProjectMilestone>
    {
        private readonly IProjectMilestoneBoundLogic _projectMilestoneBoundLogic;

        private readonly UserManager<User> _userManager;

        public ProjectMilestoneBoundController(IProjectMilestoneBoundLogic projectMilestoneBoundLogic, UserManager<User> userManager)
        {
            _projectMilestoneBoundLogic = projectMilestoneBoundLogic;
            _userManager = userManager;
        }

        protected override IBasicCrudBoundLogic<ProjectMilestone> BasicCrudUserBoundLogic()
        {
            return _projectMilestoneBoundLogic;
        }

        protected override UserManager<User> UserManager()
        {
            return _userManager;
        }
    }
}