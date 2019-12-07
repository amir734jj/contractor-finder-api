using AutoMapper;
using Dal.Interfaces;
using Logic.Abstracts;
using Logic.Interfaces;
using Models.Entities.Internals;

namespace Logic
{
    public class InternalUserLogic : BasicCrudLogicAbstract<InternalUser>, IInternalUserLogic
    {
        private readonly IInternalUserDal _internalUser;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="internalUser"></param>
        /// <param name="mapper"></param>
        public InternalUserLogic(IInternalUserDal internalUser, IMapper mapper)
        {
            _internalUser = internalUser;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns instance of DAL
        /// </summary>
        /// <returns></returns>
        protected override IBasicCrudDal<InternalUser> GetBasicCrudDal()
        {
            return _internalUser;
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