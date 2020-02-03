using System;
using Models.Entities.Users;

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

        public Guid? Photo { get; set; }
        
        public ProfileViewModel() { }
        
        public ProfileViewModel(User user) : this()
        {
            if (user == null) return;
            
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Email = user.Email;
            Role = user.Role.ToString();
            PhoneNumber = user.PhoneNumber;
            Description = user.Description;
            Photo = user.Photo;
        }
    }
}