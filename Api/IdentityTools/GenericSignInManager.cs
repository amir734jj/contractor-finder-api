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
    public class GenericSignInManager
    {
        private readonly SignInManager<Contractor> _contractorSignInManager;
        private readonly SignInManager<Homeowner> _homeownerSignInManager;
        private readonly SignInManager<InternalUser> _internalSignInManager;

        public GenericSignInManager(
            SignInManager<Contractor> contractorSignInManager,
            SignInManager<Homeowner> homeownerSignInManager,
            SignInManager<InternalUser> internalSignInManager
        )
        {
            _contractorSignInManager = contractorSignInManager;
            _homeownerSignInManager = homeownerSignInManager;
            _internalSignInManager = internalSignInManager;
        }
        
        public Func<RoleEnum, Task> SignInAsync(User user, bool isPersistent) => role => role switch
        {
            RoleEnum.Internal => _internalSignInManager.SignInAsync((InternalUser) user, isPersistent),
            RoleEnum.Contractor => _contractorSignInManager.SignInAsync((Contractor) user, isPersistent),
            RoleEnum.Homeowner => _homeownerSignInManager.SignInAsync((Homeowner) user, isPersistent),
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };

        public Func<RoleEnum, Task> SignOutAsync() => role => role switch
        {
            RoleEnum.Internal => _internalSignInManager.SignOutAsync(),
            RoleEnum.Contractor => _contractorSignInManager.SignOutAsync(),
            RoleEnum.Homeowner => _homeownerSignInManager.SignOutAsync(),
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }
}