using System.Collections.Generic;
using Models.Entities.Common;
using Models.Entities.Projects;
using Models.Entities.Users;
using Newtonsoft.Json;

namespace Models.Entities.Contractors
{
    public class Contractor : Entity
    {
        public List<ShowcaseProject> ShowcaseProjects { get; set; } = new List<ShowcaseProject>();

        public List<ProjectMilestone> HomeownerProjects { get; set; } = new List<ProjectMilestone>();

        [JsonIgnore]
        public User UserRef { get; set; }

        public List<Speciality> Speciality { get; set; } = new List<Speciality>();
    }
}