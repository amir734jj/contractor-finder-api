using System;
using System.Threading.Tasks;
using Logic.Interfaces;
using Logic.Interfaces.Basic;
using Models.Entities.Common;
using Models.Entities.Users;
using Models.ViewModels;

namespace Logic
{
    public class ProfileLogic : IProfileLogic
    {
        private readonly IUserLogic _userLogic;
        
        private readonly IDescriptivePhotoLogic _descriptivePhotoLogic;

        public ProfileLogic(IUserLogic userLogic, IDescriptivePhotoLogic descriptivePhotoLogic)
        {
            _userLogic = userLogic;
            _descriptivePhotoLogic = descriptivePhotoLogic;
        }

        public async Task Update(User user, ProfileViewModel profileViewModel)
        {
            user.Description = profileViewModel.Description;
            user.PhoneNumber = profileViewModel.PhoneNumber;
            user.Name = profileViewModel.Name;

            if (Guid.TryParse(profileViewModel.Photo, out var key) && user.PhotoRef?.Key != key)
            {
                var photo = await _descriptivePhotoLogic.Save(new DescriptivePhoto {Key = key});

                user.PhotoKey = photo.Id;
            }

            await _userLogic.Update(user.Id, user);
        }
    }
}