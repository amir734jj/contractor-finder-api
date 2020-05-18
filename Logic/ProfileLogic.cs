using System.Threading.Tasks;
using Logic.Interfaces;
using Models.Entities.Users;
using Models.ViewModels;

namespace Logic
{
    public class ProfileLogic : IProfileLogic
    {
        private readonly IUserLogic _userLogic;

        public ProfileLogic(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public async Task Update(User user, ProfileViewModel profileViewModel)
        {
            user.Description = profileViewModel.Description;
            user.PhoneNumber = profileViewModel.PhoneNumber;
            user.Firstname = profileViewModel.Firstname;
            user.Lastname = profileViewModel.Lastname;

            await _userLogic.Update(user.Id, user);
        }
    }
}