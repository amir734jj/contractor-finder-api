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
        private UserManager<Contractor> _contractorUserManager;
        private UserManager<Homeowner> _homeownerUserManager;
        private UserManager<InternalUser> _internalUserManager;
        private SignInManager<Contractor> _contractorSignManager;
        private SignInManager<Homeowner> _homeownerSignManager;
        private SignInManager<InternalUser> _internalUserSignManager;

        public AccountController(IOptions<JwtSettings> jwtSettings,
            UserManager<Contractor> contractorUserManager,
            UserManager<Homeowner> homeownerUserManager,
            UserManager<InternalUser> internalUserManager,
            SignInManager<Contractor> contractorSignManager,
            SignInManager<Homeowner> homeownerSignManager,
            SignInManager<InternalUser> internalUserSignManager
            )
        {
            _jwtSettings = jwtSettings;
            _contractorUserManager = contractorUserManager;
            _homeownerUserManager = homeownerUserManager;
            _internalUserManager = internalUserManager;
            _contractorSignManager = contractorSignManager;
            _homeownerSignManager = homeownerSignManager;
            _internalUserSignManager = internalUserSignManager;
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
            var user = Models.Entities.Users.User.New(registerViewModel.Role);

            user.Firstname = registerViewModel.Firstname;
            user.Lastname = registerViewModel.Lastname;
            user.UserName = registerViewModel.Username;
            user.Email = registerViewModel.Email;

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

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
        public async Task<IActionResult> Logout(RoleEnum roleEnum)
        {
            switch (roleEnum)
            {
                case RoleEnum.Internal:
                    await _internalUserSignManager.SignOutAsync();
                    break;
                case RoleEnum.Contractor:
                    await _contractorSignManager.SignOutAsync();
                    break;
                case RoleEnum.Homeowner:
                    await _homeownerSignManager.SignOutAsync();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(roleEnum), roleEnum, null);
            }

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