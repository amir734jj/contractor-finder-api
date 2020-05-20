using System.Threading.Tasks;
using Api.Abstracts;
using Logic.Interfaces;
using Logic.Interfaces.Basic;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Contractors;

namespace Api.Controllers.Crud.Basic
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
        
        protected override async Task<IBasicCrudLogic<Contractor>> BasicCrudLogic()
        {
            return _contractorLogic;
        }
    }
}