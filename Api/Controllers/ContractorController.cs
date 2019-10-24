using API.Abstracts;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ContractorController : BasicCrudController<Contractor>
    {
        private readonly IContractorLogic _contractorLogic;

        public ContractorController(IContractorLogic contractorLogic)
        {
            _contractorLogic = contractorLogic;
        }
        
        protected override IBasicCrudLogic<Contractor> BasicCrudLogic()
        {
            return _contractorLogic;
        }
    }
}