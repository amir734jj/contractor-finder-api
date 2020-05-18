using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly RoleManager<UserRole> _roleManager;

        public AccountController(IOptions<JwtSettings> jwtSettings, UserManager<User> userManager,
            SignInManager<User> signManager, RoleManager<UserRole> roleManager)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _signManager = signManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("")]
        [SwaggerOperation("AccountInfo")]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(await _userManager.FindByEmailAsync(User.Identity.Name));
            }

            return Ok(new { });
        }

        [HttpPost]
        [Route("Register/{role}")]
        [SwaggerOperation("Register")]
        public async Task<IActionResult> Register([FromRoute] RoleEnum role,
            [FromBody] RegisterViewModel registerViewModel)
        {
            var user = new User
            {
                Firstname = registerViewModel.Firstname,
                Lastname = registerViewModel.Lastname,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Username,
                Role = role
            };

            switch (role)
            {
                case RoleEnum.Internal:
                    user.InternalUserRef = new InternalUser();
                    break;
                case RoleEnum.Contractor:
                    user.ContractorRef = new Contractor();
                    break;
                case RoleEnum.Homeowner:
                    user.HomeownerRef = new Homeowner();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(role), role, null);
            }

            // Create user
            var identityResults = new List<IdentityResult>
            {
                await _userManager.CreateAsync(user, registerViewModel.Password)
            };

            // Create the role if not exist
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                identityResults.Add(await _roleManager.CreateAsync(new UserRole(role.ToString())));
            }

            // Register the user to the role
            identityResults.Add(await _userManager.AddToRoleAsync(user, role.ToString()));

            if (identityResults.Aggregate(true, (b, result) => b && result.Succeeded))
            {
                return Ok("Successfully registered!");
            }

            return BadRequest("Failed to register!");
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
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtSettings.Value.Issuer,
                _jwtSettings.Value.Issuer,
                claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.Value.AccessTokenDurationInMinutes),
                signingCredentials: credentials);

            var userRoleInfo = await _userManager.GetRolesAsync(user);
  
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                roles = userRoleInfo,
                user.Role,
                user.Firstname,
                user.Lastname,
                user.Email
            });
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