using Dal.Interfaces;
using Logic.Abstracts;
using Logic.Interfaces;
using Models.Entities.Internals;

namespace Logic.Crud
{
    public class InternalUserLogic : BasicCrudLogicAbstract<InternalUser>, IInternalUserLogic
    {
        private readonly IInternalUserDal _internalUser;

        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="internalUser"></param>
        public InternalUserLogic(IInternalUserDal internalUser)
        {
            _internalUser = internalUser;
        }

        /// <summary>
        /// Returns instance of DAL
        /// </summary>
        /// <returns></returns>
        protected override IBasicCrudDal<InternalUser> GetBasicCrudDal()
        {
            return _internalUser;
        }
    }
}