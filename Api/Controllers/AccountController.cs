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
using Models.Entities.Users;
using Models.Enums;
using Models.Factories;
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
                : Ok(new { });
        }

        [HttpPost]
        [Route("Register/{role}")]
        [SwaggerOperation("Register")]
        public async Task<IActionResult> Register([FromRoute]RoleEnum role, [FromBody] RegisterViewModel registerViewModel)
        {
            var user = UserFactory.New(role, x =>
            {
                x.Firstname = registerViewModel.Firstname;
                x.Lastname = registerViewModel.Lastname;
                x.Email = registerViewModel.Email;
                x.UserName = registerViewModel.Username;
            });

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            await _userManager.AddToRoleAsync(user, role.ToString());

            return result.Succeeded ? (IActionResult) Ok("Successfully registered!") : BadRequest("Failed to register!");
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

            var token = new JwtSecurityToken(
                _jwtSettings.Value.Issuer,
                _jwtSettings.Value.Issuer,
                claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.Value.AccessTokenDurationInMinutes),
                signingCredentials: credentials);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpPost]
        [Route("Logout")]
        [SwaggerOperation("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();

            return Ok("Logged-Out");
        }
    }
}