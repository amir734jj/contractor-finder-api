using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Models.Entities.Common;
using Models.Entities.Contractors;
using Models.Entities.Homeowners;

namespace Models.Entities.Projects
{
    public class ProjectMilestone : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }
        
        public DateTimeOffset DateTime { get; set; }

        public Contractor Contractor { get; set; }

        public Homeowner Homeowner { get; set; }

        public List<ProjectPhoto> ProjectPhotos { get; set; } = new List<ProjectPhoto>();
    }
}