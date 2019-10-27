using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using Models.Entities.UserEntities;
using Models.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        private readonly IOptions<JwtSettings> _jwtSettings;

        public AccountController(IOptions<JwtSettings> jwtSettings, UserManager<User> userManager,
            SignInManager<User> signManager)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _signManager = signManager;
        }

        [HttpGet]
        [Route("")]
        [SwaggerOperation("AccountInfo")]
        public async Task<IActionResult> Index()
        {
            return User.Identity.IsAuthenticated
                ? Ok(await _userManager.FindByEmailAsync(User.Identity.Name))
                : Ok(new object());
        }

        [HttpGet]
        [Route("Register")]
        [SwaggerOperation("Register")]
        public async Task<IActionResult> RegisterIndex()
        {
            return Ok("Please log-in by posting to this route!");
        }

        [HttpPost]
        [Route("Register")]
        [SwaggerOperation("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            var user = new User
            {
                Firstname = registerViewModel.Firstname,
                Lastname = registerViewModel.Lastname,
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email
            };
            
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            return result.Succeeded ? (IActionResult) Ok(new { user.Email, user.UserName }) : BadRequest("Failed to register!");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Route("Login")]
        [SwaggerOperation("Login")]
        public async Task<IActionResult> Login()
        {
            return Ok("Please log-in by posting to this route!");
        }

        [HttpPost]
        [Route("Login")]
        [SwaggerOperation("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            // Ensure the username and password is valid.
            var user = await _userManager.FindByNameAsync(loginViewModel.Username);
            
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginViewModel.Password))
            {
                return BadRequest(new
                {
                    error = "", // OpenIdConnectConstants.Errors.InvalidGrant,
                    error_description = "The username or password is invalid."
                });
            }

            await _signManager.SignInAsync(user, true);

            // Generate and issue a JWT token
            var claims = new [] {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
          
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenObject = new JwtSecurityToken(
                _jwtSettings.Value.Issuer,
                _jwtSettings.Value.Issuer,
                claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.Value.AccessTokenDurationInMinutes),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            var email = user.Email;

            return Ok(new { token, email });
        }

        [HttpGet]
        [Route("Logout")]
        [SwaggerOperation("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();

            return Ok("Logged-Out");
        }

        [HttpGet]
        [Route("Forbidden")]
        [SwaggerOperation("Forbidden")]
        public async Task<IActionResult> Forbidden()
        {
            return Ok("Forbidden");
        }
    }
}