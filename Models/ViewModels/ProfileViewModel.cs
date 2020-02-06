using System;
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

        public RoleEnum Role { get; set; }

        public Guid? Photo { get; set; }
        
        public ProfileViewModel() { }
        
        public ProfileViewModel(User user) : this()
        {
            if (user == null) return;
            
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Email = user.Email;
            Role = user.Role;
            PhoneNumber = user.PhoneNumber;
            Description = user.Description;
            Photo = user.Photo;
        }
    }
}