using Dal.Interfaces;
using Logic.Abstracts;
using Logic.Interfaces;
using Models.Entities.Homeowners;

namespace Logic.Crud
{
    public class HomeownerLogic : BasicCrudLogicAbstract<Homeowner>, IHomeownerLogic
    {
        private readonly IHomeownerDal _homeownerDal;

        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="homeownerDal"></param>
        public HomeownerLogic(IHomeownerDal homeownerDal)
        {
            _homeownerDal = homeownerDal;
        }

        /// <summary>
        /// Returns instance of DAL
        /// </summary>
        /// <returns></returns>
        protected override IBasicCrudDal<Homeowner> GetBasicCrudDal()
        {
            return _homeownerDal;
        }
    }
}