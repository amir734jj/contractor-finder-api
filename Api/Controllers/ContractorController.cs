using Api.Abstracts;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Contractors;
using Models.Enums;

namespace Api.Controllers
{
    [Authorize(Roles = "Internal,Contractor,Homeowner")]
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