using System.Threading.Tasks;
using Api.Abstracts;
using Api.Attributes;
using Logic.Interfaces;
using Logic.Interfaces.Basic;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Users;
using static Models.Enums.RoleEnum;

namespace Api.Controllers.Crud.Basic
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

        protected override async Task<IBasicCrudLogic<User>> BasicCrudLogic()
        {
            return _userLogic;
        }
    }
}