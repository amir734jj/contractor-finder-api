using Microsoft.AspNetCore.Authorization;
using Models.Enums;

namespace Api.Attributes
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public RoleAuthorizeAttribute(params RoleEnum[] roles)
        {
            Roles = string.Join(',', roles);
        }
    }
}