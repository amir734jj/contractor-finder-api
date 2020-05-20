using Models.Entities.Common;
using Models.Entities.Users;

namespace Models.Entities.Internals
{
    public class InternalUser : Entity
    {
        public User UserRef { get; set; }
    }
}