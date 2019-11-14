using System;
using Dal.Abstracts;
using Dal.Utilities;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Internals;
using Models.Entities.Users;

namespace Dal.IdentityStores
{
    public class InternalUserStore : AbstractUserStore<InternalUser, UserRole, Guid>
    {
        public InternalUserStore(EntityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}