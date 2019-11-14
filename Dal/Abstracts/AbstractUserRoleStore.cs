using System;
using Dal.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dal.Abstracts
{
    public abstract class AbstractUserRoleStore<TUserRole, TKey> : RoleStore<TUserRole, EntityDbContext, TKey>
        where TUserRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        protected AbstractUserRoleStore(EntityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}