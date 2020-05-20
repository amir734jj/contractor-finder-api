using Dal.Interfaces;
using Logic.Abstracts;
using Logic.Interfaces.Basic;
using Models.Entities.Users;

namespace Logic.Crud.Basic
{
    public class UserLogic : BasicCrudLogicAbstract<User>, IUserLogic
    {
        private readonly IUserDal _userDal;

        public UserLogic(IUserDal userDal)
        {
            _userDal = userDal;
        }
        
        protected override IBasicCrudDal<User> GetBasicCrudDal()
        {
            return _userDal;
        }
    }
}