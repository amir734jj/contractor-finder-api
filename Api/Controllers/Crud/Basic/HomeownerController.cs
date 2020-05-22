using System.Threading.Tasks;
using Api.Abstracts;
using Api.Attributes;
using Logic.Interfaces;
using Logic.Interfaces.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Homeowners;
using Models.Entities.Users;
using Models.Enums;

namespace Api.Controllers.Crud.Basic
{
    [RoleAuthorize(RoleEnum.Internal)]
    [Route("Api/[controller]")]
    public class HomeownerController : BasicCrudController<Homeowner>
    {
        private readonly IHomeownerLogic _homeownerLogic;

        public HomeownerController(IHomeownerLogic homeownerLogic)
        {
            _homeownerLogic = homeownerLogic;
        }
        
        protected override async Task<IBasicCrudLogic<Homeowner>> BasicCrudLogic()
        {
            return _homeownerLogic;
        }
    }
}