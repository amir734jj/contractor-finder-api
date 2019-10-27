using System;
using System.Threading.Tasks;
using Api.Abstracts;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.ContractorEntities;

namespace Api.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "Admin,Moderator")]
        public override Task<IActionResult> Save(Contractor instance)
        {
            return base.Save(instance);
        }

        [Authorize(Roles = "Admin,Moderator")]
        public override Task<IActionResult> Update(Guid id, Contractor instance)
        {
            return base.Update(id, instance);
        }
    }
}