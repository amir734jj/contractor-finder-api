using System;
using System.Collections.Generic;
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

        public List<DescriptivePhoto> ProjectPhotos { get; set; } = new List<DescriptivePhoto>();
    }
}