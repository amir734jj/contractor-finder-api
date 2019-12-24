using Models.Entities.Common;
using Models.Entities.Users;
using Models.Enums;

namespace Models.ViewModels
{
    public class Profile
    {
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
        
        public string Email { get; set; }

        public string Role { get; set; }

        public ProfilePhoto ProfilePhoto { get; set; }
        
        public Profile() { }
        
        public Profile(User user) : this()
        {
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Email = user.Email;
            Role = user.Role.ToString();
            ProfilePhoto = user.ProfilePhoto;
        }
    }
}