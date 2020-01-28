using Models.Entities.Common;
using Models.Entities.Users;
using Models.Enums;

namespace Models.ViewModels
{
    public class ProfileViewModel
    {
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Description { get; set; }

        public string Role { get; set; }

        public ProfilePhoto ProfilePhoto { get; set; }
        
        public ProfileViewModel() { }
        
        public ProfileViewModel(User user) : this()
        {
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Email = user.Email;
            Role = user.Role.ToString();
            PhoneNumber = user.PhoneNumber;
            Description = user.Description;
            ProfilePhoto = user.ProfilePhoto;
        }
    }
}