using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Models.Enums;

namespace Models.Entities.Users
{
    public class UserRole : IdentityRole<Guid>
    {
        public UserRole(string name) : base(name) { }
        
        [NotMapped]
        public RoleEnum Role => Enum.Parse<RoleEnum>(Name);
    }
}