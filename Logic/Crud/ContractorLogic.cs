using Dal.Interfaces;
using Logic.Abstracts;
using Logic.Interfaces;
using Models.Entities.Contractors;

namespace Logic.Crud
{
    public class ContractorLogic : BasicCrudLogicAbstract<Contractor>, IContractorLogic
    {
        private readonly IContractorDal _contractorDal;

        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="contractorDal"></param>
        public ContractorLogic(IContractorDal contractorDal)
        {
            _contractorDal = contractorDal;
        }

        /// <summary>
        /// Returns instance of driver DAL
        /// </summary>
        /// <returns></returns>
        protected override IBasicCrudDal<Contractor> GetBasicCrudDal()
        {
            return _contractorDal;
        }
    }
}