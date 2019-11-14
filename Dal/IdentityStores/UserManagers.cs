using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models.Entities.Contractors;
using Models.Entities.Homeowners;
using Models.Entities.Internals;

namespace Dal.IdentityStores
{
    public class ContractorUserManager : UserManager<Contractor>
    {
        public ContractorUserManager(IUserStore<Contractor> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Contractor> passwordHasher, IEnumerable<IUserValidator<Contractor>> userValidators, IEnumerable<IPasswordValidator<Contractor>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Contractor>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
    
    public class HomeownerUserManager : UserManager<Homeowner>
    {
        public HomeownerUserManager(IUserStore<Homeowner> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Homeowner> passwordHasher, IEnumerable<IUserValidator<Homeowner>> userValidators, IEnumerable<IPasswordValidator<Homeowner>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Homeowner>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
    
    public class InternalUserUserManager : UserManager<InternalUser>
    {
        public InternalUserUserManager(IUserStore<InternalUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<InternalUser> passwordHasher, IEnumerable<IUserValidator<InternalUser>> userValidators, IEnumerable<IPasswordValidator<InternalUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<InternalUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}