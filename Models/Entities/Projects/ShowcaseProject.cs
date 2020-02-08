using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Models.Entities.Common;
using Models.Entities.Contractors;
using Models.Interfaces;

namespace Models.Entities.Projects
{
    public class ShowcaseProject : IEntity
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "jsonb")]
        public List<ProjectPhoto> ProjectPhotos { get; set; } = new List<ProjectPhoto>();

        [JsonIgnore]
        public Contractor Contractor { get; set; }
    }
}