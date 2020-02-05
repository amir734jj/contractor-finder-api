using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Models.Entities.Common;
using Models.Entities.Contractors;
using Models.Entities.Homeowners;
using Models.Interfaces;

namespace Models.Entities.Projects
{
    public class ProjectMilestone : IEntity
    {
        public Guid Id { get; set; }
                
        public string Title { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public Contractor Contractor { get; set; }
        
        [JsonIgnore]
        public Homeowner Homeowner { get; set; }

        [Column(TypeName = "jsonb")]
        public List<ProjectPhoto> ProjectPhotos { get; set; }
    }
}