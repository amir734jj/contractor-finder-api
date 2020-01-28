using System.Threading.Tasks;
using Models.Entities.Users;
using Models.ViewModels;

namespace Logic.Interfaces
{
    public interface IProfileLogic
    {
        Task Update(User user, ProfileViewModel profileViewModel);
    }
}