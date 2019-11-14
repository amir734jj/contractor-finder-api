using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Configs;
using Api.IdentityTools;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using Models.Entities.Contractors;
using Models.Entities.Homeowners;
using Models.Entities.Internals;
using Models.Entities.Users;
using Models.Enums;
using Models.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly GenericUserManager _genericUserManager;
        private readonly GenericSignInManager _genericSignInManager;

        public AccountController(IOptions<JwtSettings> jwtSettings,
            GenericUserManager genericUserManager,
            GenericSignInManager genericSignInManager
            )
        {
            _jwtSettings = jwtSettings;
            _genericUserManager = genericUserManager;
            _genericSignInManager = genericSignInManager;
        }

        [HttpGet]
        [Route("{role}")]
        [SwaggerOperation("AccountInfo")]
        public async Task<IActionResult> Index([FromRoute] RoleEnum role)
        {
            return User.Identity.IsAuthenticated
                ? Ok(await _genericUserManager.FindByNameAsync(User.Identity.Name)(role))
                : Ok(new { });
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
            var user = Models.Entities.Users.User.New(registerViewModel.Role);

            user.Firstname = registerViewModel.Firstname;
            user.Lastname = registerViewModel.Lastname;
            user.UserName = registerViewModel.Username;
            user.Email = registerViewModel.Email;

            var result = await _genericUserManager.CreateAsync(user, registerViewModel.Password)(registerViewModel.Role);

            return result.Succeeded
                ? (IActionResult) Ok(new {user.Email, user.UserName})
                : BadRequest("Failed to register!");
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
            var user = await _genericUserManager.FindByNameAsync(loginViewModel.Username)(loginViewModel.Role);

            if (user == null || !await _genericUserManager.CheckPasswordAsync(user, loginViewModel.Password)(loginViewModel.Role))
            {
                return BadRequest(new
                {
                    error = "", // OpenIdConnectConstants.Errors.InvalidGrant,
                    error_description = "The username or password is invalid."
                });
            }

            await _genericSignInManager.SignInAsync(user, true)(loginViewModel.Role);

            // Generate and issue a JWT token
            var claims = new[]
            {
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

            return Ok(new {token, email});
        }

        [HttpGet]
        [Route("Logout/{roleEnum}")]
        [SwaggerOperation("Logout")]
        public async Task<IActionResult> Logout([FromRoute] RoleEnum roleEnum)
        {
            await _genericSignInManager.SignOutAsync()(roleEnum);

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