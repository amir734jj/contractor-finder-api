using System;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Common;
using Models.Interfaces;

namespace Models.Entities.Users
{
    public class User : IdentityUser<Guid>, IPerson
    {
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
        
        public ProfilePhoto ProfilePhoto { get; set; }
    }
}