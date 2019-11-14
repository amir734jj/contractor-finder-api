using Models.Entities.Users;
using Models.Enums;

namespace Models.Entities.Internals
{
    public class InternalUser : User
    {
        public override RoleEnum ResolveRole()
        {
            return RoleEnum.Internal;
        }
    }
}