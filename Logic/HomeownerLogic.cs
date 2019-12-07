using AutoMapper;
using Dal.Interfaces;
using Logic.Abstracts;
using Logic.Interfaces;
using Models.Entities.Homeowners;

namespace Logic
{
    public class HomeownerLogic : BasicCrudLogicAbstract<Homeowner>, IHomeownerLogic
    {
        private readonly IHomeownerDal _homeownerDal;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="homeownerDal"></param>
        /// <param name="mapper"></param>
        public HomeownerLogic(IHomeownerDal homeownerDal, IMapper mapper)
        {
            _homeownerDal = homeownerDal;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns instance of DAL
        /// </summary>
        /// <returns></returns>
        protected override IBasicCrudDal<Homeowner> GetBasicCrudDal()
        {
            return _homeownerDal;
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