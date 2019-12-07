using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Models.Entities.Homeowners;
using Models.Interfaces;

namespace Models.Entities.Projects
{
    public class HomeownerProject : IEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public List<ProjectMilestone> Milestones { get; set; }
        
        [JsonIgnore]
        public Homeowner Homeowner { get; set; }
    }
}