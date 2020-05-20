using System.Threading.Tasks;
using Api.Abstracts;
using Logic.Interfaces;
using Logic.Interfaces.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Homeowners;

namespace Api.Controllers.Crud.Basic
{
    [Authorize]
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