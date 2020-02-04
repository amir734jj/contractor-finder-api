using Api.Abstracts;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Users;

namespace Api.Controllers
{
    [Authorize(Roles = "Internal")]
    [ApiController]
    [Route("Api/[controller]")]
    public class UserController : BasicCrudController<User>
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        protected override IBasicCrudLogic<User> BasicCrudLogic()
        {
            return _userLogic;
        }
    }
}