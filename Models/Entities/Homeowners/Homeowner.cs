using System.Collections.Generic;
using System.Text.Json.Serialization;
using Models.Entities.Common;
using Models.Entities.Projects;
using Models.Entities.Users;

namespace Models.Entities.Homeowners
{
    public class Homeowner : Entity
    {
        public string Address { get; set; }
        
        public List<HomeownerProject> Projects { get; set; } = new List<HomeownerProject>();

        public User UserRef { get; set; }
    }
}