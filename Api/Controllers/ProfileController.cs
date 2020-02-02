using System.Threading.Tasks;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Users;
using Models.ViewModels;

namespace Api.Controllers
{
    [EnableCors]
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        
        private readonly IProfileLogic _profileLogic;

        public ProfileController(UserManager<User> userManager, IProfileLogic profileLogic)
        {
            _userManager = userManager;
            _profileLogic = profileLogic;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            
            return Ok(new ProfileViewModel(user));
        }
        
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Update([FromBody] ProfileViewModel profileViewModel)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            await _profileLogic.Update(user, profileViewModel);

            return Ok(profileViewModel);
        }
    }
}