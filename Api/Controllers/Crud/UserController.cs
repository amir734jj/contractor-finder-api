using Api.Abstracts;
using Api.Attributes;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Users;
using static Models.Enums.RoleEnum;

namespace Api.Controllers.Crud
{
    [RoleAuthorize(Internal)]
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