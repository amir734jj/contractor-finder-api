using System.Threading.Tasks;
using Models.Entities;
using Models.Enums;

namespace Logic.Interfaces
{
    public interface IUserLogic : IBasicCrudLogic<User>
    {
        Task<User> UpdateUserRole(int id, UserRoleEnum userRoleEnum);
    }
}