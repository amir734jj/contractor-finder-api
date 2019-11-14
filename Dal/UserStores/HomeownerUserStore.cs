using System;
using Dal.Abstracts;
using Dal.Utilities;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Homeowners;
using Models.Entities.Users;

namespace Dal
{
    public class HomeownerUserStore : AbstractUserStore<Homeowner, UserRole, Guid>
    {
        public HomeownerUserStore(EntityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}