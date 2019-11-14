using System;
using Dal.Abstracts;
using Dal.Utilities;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Contractors;
using Models.Entities.Internals;
using Models.Entities.Users;

namespace Dal
{
    public class AdminUserStore : AbstractUserStore<AdminUser, UserRole, Guid>
    {
        public AdminUserStore(EntityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}