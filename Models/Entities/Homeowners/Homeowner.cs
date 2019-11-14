using System.Collections.Generic;
using Models.Abstracts;
using Models.Entities.Projects;
using Models.Entities.Users;
using Models.Enums;

namespace Models.Entities.Homeowners
{
    public class Homeowner : User
    {
        public string Address { get; set; }
        
        public List<HomeownerProject> Projects { get; set; }
        
        public override RoleEnum ResolveRole()
        {
            return RoleEnum.Homeowner;
        }
    }
}