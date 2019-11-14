using System;
using Dal.Abstracts;
using Dal.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityWithStore<TUser, TRole, TUserStore, TRoleStore>(
            this IServiceCollection services)
            where TUser : class
            where TRole : class
            where TUserStore: IUserStore<TUser>
            where TRoleSore : IRoleStore<>
        {
            services
                .AddIdentityCore<TUser>()
                .AddEntityFrameworkStores<EntityDbContext>()
                .AddRoles<TRole>()
                .AddUserStore<TUserStore>()
                .AddRoleStore<TRole>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}