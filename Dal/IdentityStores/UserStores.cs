using System;
using Dal.Abstracts;
using Dal.Utilities;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Contractors;
using Models.Entities.Homeowners;
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
    
    public class HomeownerUserStore : AbstractUserStore<Homeowner, UserRole, Guid>
    {
        public HomeownerUserStore(EntityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
    
    public class ContractorUserStore : AbstractUserStore<Contractor, UserRole, Guid>
    {
        public ContractorUserStore(EntityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}