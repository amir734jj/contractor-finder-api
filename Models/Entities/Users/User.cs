using System;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Common;
using Models.Entities.Contractors;
using Models.Entities.Homeowners;
using Models.Entities.Internals;
using Models.Enums;
using Models.Interfaces;

namespace Models.Entities.Users
{
    public abstract class User : IdentityUser<Guid>, IPerson
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public ProfilePhoto ProfilePhoto { get; set; }

        public abstract RoleEnum ResolveRole();

        public static User New(RoleEnum roleEnum)
        {
            return roleEnum switch
            {
                RoleEnum.Internal => (User) new InternalUser(),
                RoleEnum.Contractor => new Contractor(),
                RoleEnum.Homeowner => new Homeowner(),
                _ => throw new ArgumentOutOfRangeException(nameof(roleEnum), roleEnum, null)
            };
        }
    }
}