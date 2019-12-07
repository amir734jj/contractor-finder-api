using AutoMapper;
using Dal.Interfaces;
using Logic.Abstracts;
using Logic.Interfaces;
using Models.Entities.Contractors;

namespace Logic
{
    public class ContractorLogic : BasicCrudLogicAbstract<Contractor>, IContractorLogic
    {
        private readonly IContractorDal _contractorDal;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="contractorDal"></param>
        /// <param name="mapper"></param>
        public ContractorLogic(IContractorDal contractorDal, IMapper mapper)
        {
            _contractorDal = contractorDal;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns instance of driver DAL
        /// </summary>
        /// <returns></returns>
        protected override IBasicCrudDal<Contractor> GetBasicCrudDal()
        {
            return _contractorDal;
        }

        /// <summary>
        /// Returns AutoMapper instance
        /// </summary>
        /// <returns></returns>
        protected override IMapper Mapper()
        {
            return _mapper;
        }
    }
}