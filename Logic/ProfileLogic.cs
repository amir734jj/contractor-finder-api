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
            await _userLogic.Update(user.Id, entity =>
            {
                entity.Description = profileViewModel.Description;
                entity.PhoneNumber = profileViewModel.PhoneNumber;
                entity.Firstname = profileViewModel.Firstname;
                entity.Lastname = profileViewModel.Lastname;
                entity.Description = profileViewModel.Description;
                entity.Photo = profileViewModel.Photo;

                entity.ContractorRef.Speciality = profileViewModel.Contractor?.Speciality;
            });
        }
    }
}