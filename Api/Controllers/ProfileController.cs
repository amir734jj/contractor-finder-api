using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Users;
using Models.ViewModels;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;

        public ProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            
            return Ok(new Profile(user));
        }
        
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Update([FromBody] Profile profile)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            
            // _userManager.up
            
            return Ok(new Profile(user));
        }
    }
}