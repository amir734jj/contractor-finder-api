using System.Threading.Tasks;
using Api.Attributes;
using Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Users;
using Models.Enums;
using Models.ViewModels.Management.Homeowner;

namespace Api.Controllers.Crud
{
    [RoleAuthorize(RoleEnum.Homeowner)]
    [Route("api/manage/homeowner")]
    public class HomeownerManageController : Controller
    {
        private readonly IHomeownerManageLogic _homeownerManageLogic;
        private readonly UserManager<User> _userManager;

        public HomeownerManageController(IHomeownerManageLogic homeownerManageLogic, UserManager<User> userManager)
        {
            _homeownerManageLogic = homeownerManageLogic;
            _userManager = userManager;
        }
        
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            var profile = await _homeownerManageLogic.ResolveProfile(user);
            
            return Ok(profile);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Update([FromBody] HomeownerExtendedProfileViewModel profile)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            var result = await _homeownerManageLogic.UpdateProfile(user, profile);
            
            return Ok(result);
        }
    }
}