using Api.Abstracts;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Homeowners;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class HomeownerController : BasicCrudController<Homeowner>
    {
        private readonly IHomeownerLogic _homeownerLogic;

        public HomeownerController(IHomeownerLogic homeownerLogic)
        {
            _homeownerLogic = homeownerLogic;
        }
        
        protected override IBasicCrudLogic<Homeowner> BasicCrudLogic()
        {
            return _homeownerLogic;
        }
    }
}