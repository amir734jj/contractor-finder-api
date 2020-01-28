using AutoMapper;
using Dal.Interfaces;
using Logic.Abstracts;
using Logic.Interfaces;
using Models.Entities.Users;

namespace Logic
{
    public class UserLogic : BasicCrudLogicAbstract<User>, IUserLogic
    {
        private readonly IMapper _mapper;
        
        private readonly IUserDal _userDal;

        public UserLogic(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }
        
        protected override IBasicCrudDal<User> GetBasicCrudDal()
        {
            return _userDal;
        }

        protected override IMapper Mapper()
        {
            return _mapper;
        }
    }
}