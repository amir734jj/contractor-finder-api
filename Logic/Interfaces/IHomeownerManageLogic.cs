using System.Threading.Tasks;
using Models.Entities.Users;
using Models.ViewModels.Management.Homeowner;

namespace Logic.Interfaces
{
    public interface IHomeownerManageLogic
    {
        Task<HomeownerExtendedProfileViewModel> ResolveProfile(User user);

        Task<HomeownerExtendedProfileViewModel> UpdateProfile(User user, HomeownerExtendedProfileViewModel profile);
    }
}