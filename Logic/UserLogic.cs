using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using Logic.Abstracts;
using Logic.Interfaces;
using Models.Entities;
using Models.Enums;
using static Logic.Utilities.HashingUtility;

namespace Logic
{
    public class UserLogic : BasicCrudLogicAbstract<User>, IUserLogic
    {
        private readonly IUserDal _userDal;

        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="userDal"></param>
        public UserLogic(IUserDal userDal)
        {
            _userDal = userDal;
        }

        /// <summary>
        /// Returns instance of user DAL
        /// </summary>
        /// <returns></returns>
        protected override IBasicCrudDal<User> GetBasicCrudDal()
        {
            return _userDal;
        }

        /// <summary>
        /// Override
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<User> Save(User instance)
        {
            var existingUsers = (await GetAll()).ToList();
            
            // Make sure username is not duplicate
            if (existingUsers.Any(x => x.Username == instance.Username))
            {
                return null;
            }

            // First user is Admin user
            instance.UserRoleEnum = existingUsers.Any() ? UserRoleEnum.Basic : UserRoleEnum.Admin;
            
            // Do not store the plain-text password
            instance.Password = SecureHashPassword(instance.Password);
            
            return await base.Save(instance);
        }

        /// <summary>
        /// Updates user role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRoleEnum"></param>
        /// <returns></returns>
        public async Task<User> UpdateUserRole(int id, UserRoleEnum userRoleEnum)
        {
            // Update UserRole
            return await _userDal.Update(id, user => user.UserRoleEnum = userRoleEnum);
        }
    }
}