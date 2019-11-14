using System;
using Dal.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dal.Abstracts
{
    public class AbstractUserStore<TUser, TRole, TKey> : UserStore<TUser, TRole, EntityDbContext, TKey>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        protected AbstractUserStore(EntityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
            
        }
    }
}