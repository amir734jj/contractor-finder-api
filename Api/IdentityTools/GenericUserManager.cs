using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Contractors;
using Models.Entities.Homeowners;
using Models.Entities.Internals;
using Models.Entities.Users;
using Models.Enums;

namespace Api.IdentityTools
{
    public class GenericUserManager
    {
        private readonly UserManager<Contractor> _contractorUserManager;
        private readonly UserManager<Homeowner> _homeownerUserManager;
        private readonly UserManager<InternalUser> _internalUserManager;

        public GenericUserManager(
            UserManager<Contractor> contractorUserManager,
            UserManager<Homeowner> homeownerUserManager,
            UserManager<InternalUser> internalUserManager
        )
        {
            _contractorUserManager = contractorUserManager;
            _homeownerUserManager = homeownerUserManager;
            _internalUserManager = internalUserManager;
        }

        public Func<RoleEnum, Task<bool>> CheckPasswordAsync(User user, string password) => async role => role switch
        {
            RoleEnum.Internal => await _internalUserManager.CheckPasswordAsync((InternalUser) user, password),
            RoleEnum.Contractor => await _contractorUserManager.CheckPasswordAsync((Contractor) user, password),
            RoleEnum.Homeowner => await _homeownerUserManager.CheckPasswordAsync((Homeowner) user, password),
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };

        public Func<RoleEnum, Task<IdentityResult>> CreateAsync(User user, string password) => async role => role switch
        {
            RoleEnum.Internal => await _internalUserManager.CreateAsync((InternalUser) user, password),
            RoleEnum.Contractor => await _contractorUserManager.CreateAsync((Contractor) user, password),
            RoleEnum.Homeowner => await _homeownerUserManager.CreateAsync((Homeowner) user, password),
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };

        public Func<RoleEnum, Task<User>> FindByNameAsync(string email) => async role => role switch
        {
            RoleEnum.Internal => (User) await _internalUserManager.FindByNameAsync(email),
            RoleEnum.Contractor => await _contractorUserManager.FindByNameAsync(email),
            RoleEnum.Homeowner => await _homeownerUserManager.FindByNameAsync(email),
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }
}