using System;
using Dal.Abstracts;
using Dal.Utilities;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Contractors;
using Models.Entities.Users;

namespace Dal
{
    public class ContractorUserStore : AbstractUserStore<Contractor, UserRole, Guid>
    {
        public ContractorUserStore(EntityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}