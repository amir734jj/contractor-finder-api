using System.Collections.Generic;
using System.Text.Json.Serialization;
using Models.Entities.Common;
using Models.Entities.Homeowners;

namespace Models.Entities.Projects
{
    public class HomeownerProject : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }
        
        public List<ProjectMilestone> Milestones { get; set; } = new List<ProjectMilestone>();

        public Homeowner Homeowner { get; set; }
    }
}