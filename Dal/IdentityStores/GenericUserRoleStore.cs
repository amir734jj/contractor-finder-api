using System;
using Dal.Abstracts;
using Dal.Utilities;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Users;

namespace Dal.IdentityStores
{
    public class GenericUserRoleStore : AbstractUserRoleStore<UserRole, Guid>
    {
        public GenericUserRoleStore(EntityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}