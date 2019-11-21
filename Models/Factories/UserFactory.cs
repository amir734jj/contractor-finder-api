using System;
using Models.Entities.Contractors;
using Models.Entities.Homeowners;
using Models.Entities.Internals;
using Models.Entities.Users;
using Models.Enums;
using static Models.Utilities.FluentUpdateAction;

namespace Models.Factories
{
    public static class UserFactory
    {
        public static User New(RoleEnum roleEnum, Action<User> update) => FluentUpdate(roleEnum switch
        {
            RoleEnum.Internal => new User {InternalUserRef = new InternalUser()},
            RoleEnum.Contractor => new User {ContractorRef = new Contractor()},
            RoleEnum.Homeowner => new User {HomeownerRef = new Homeowner()},
            _ => throw new ArgumentOutOfRangeException(nameof(roleEnum), roleEnum, null)
        }, update);
    }
}