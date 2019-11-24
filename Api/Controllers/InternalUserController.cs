using System;
using System.Threading.Tasks;
using Api.Abstracts;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Contractors;
using Models.Entities.Internals;

namespace Api.Controllers
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
        
        protected override IBasicCrudLogic<InternalUser> BasicCrudLogic()
        {
            return _internalUserLogic;
        }
    }
}