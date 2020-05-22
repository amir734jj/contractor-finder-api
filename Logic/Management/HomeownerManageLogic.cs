using System.Threading.Tasks;
using Logic.Interfaces;
using Logic.Interfaces.Basic;
using Models.Entities.Users;
using Models.ViewModels.Management.Homeowner;

namespace Logic.Management
{
    public class HomeownerManageLogic : IHomeownerManageLogic
    {
        private readonly IUserLogic _userLogic;
        private readonly IHomeownerLogic _homeownerLogic;

        public HomeownerManageLogic(IUserLogic userLogic, IHomeownerLogic homeownerLogic)
        {
            _userLogic = userLogic;
            _homeownerLogic = homeownerLogic;
        }
        
        public async Task<HomeownerExtendedProfileViewModel> ResolveProfile(User user)
        {
            var entity = await _userLogic.Get(user.Id);

            if (entity.HomeownerKey.HasValue)
            {
                return new HomeownerExtendedProfileViewModel
                {
                    Address = entity.HomeownerRef.Address
                };
            }
            
            return null;
        }

        public async Task<HomeownerExtendedProfileViewModel> UpdateProfile(User user, HomeownerExtendedProfileViewModel profile)
        {
            var entity = await _userLogic.Get(user.Id);

            if (entity.HomeownerKey.HasValue)
            {
                entity.HomeownerRef.Address = profile.Address;

                await _homeownerLogic.Update(entity.HomeownerKey.Value, entity.HomeownerRef);
            }
            
            return profile;
        }
    }
}