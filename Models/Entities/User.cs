using System;
using Microsoft.AspNetCore.Identity;
using Models.Interfaces;

namespace Models.Entities
{
    public class User : IdentityUser<Guid>, IPerson
    {
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
    }
}