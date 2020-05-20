using System.Threading.Tasks;
using Api.Abstracts;
using Logic.Interfaces;
using Logic.Interfaces.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Internals;

namespace Api.Controllers.Crud.Basic
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class InternalUserController : BasicCrudController<InternalUser>
    {
        private readonly IInternalUserLogic _internalUserLogic;

        public InternalUserController(IInternalUserLogic internalUserLogic)
        {
            _internalUserLogic = internalUserLogic;
        }
        
        protected override async Task<IBasicCrudLogic<InternalUser>> BasicCrudLogic()
        {
            return _internalUserLogic;
        }
    }
}