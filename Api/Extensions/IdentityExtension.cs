using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities.Users;

namespace Api.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityWithStore<TUser, TRole, TUserStore>(
            this IServiceCollection services)
            where TUser : class
            where TRole : class
            where TUserStore : class, IUserStore<TUser>
            // where TRoleStore : class, IRoleStore<TRole>
        {
            services
                .AddIdentity<User, TRole>()
                .AddUserStore<TUserStore>()
                // .AddRoleStore<TRoleStore>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}