using System;
using Models.Entities.Contractors;
using Models.Entities.Homeowners;
using Models.Entities.Internals;
using Models.Entities.Users;
using Models.Enums;

namespace Models.ViewModels
{
    public class ProfileViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Description { get; set; }

        public RoleEnum Role { get; set; }

        public Guid? Photo { get; set; }
        
        public Contractor Contractor { get; set; }
        
        public InternalUser InternalUser { get; set; }
        
        public Homeowner Homeowner { get; set; }
        
        public ProfileViewModel() { }
        
        public ProfileViewModel(User user) : this()
        {
            if (user == null) return;
            
            Name = user.Name;
            Email = user.Email;
            Role = user.Role;
            PhoneNumber = user.PhoneNumber;
            Description = user.Description;
            // Photo = user.Photo;

            Contractor = user.ContractorRef;
            InternalUser = user.InternalUserRef;
            Homeowner = user.HomeownerRef;
        }
    }
}