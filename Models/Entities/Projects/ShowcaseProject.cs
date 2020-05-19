using System.Collections.Generic;
using System.Text.Json.Serialization;
using Models.Entities.Common;
using Models.Entities.Contractors;

namespace Models.Entities.Projects
{
    public class ShowcaseProject : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<ProjectPhoto> ProjectPhotos { get; set; } = new List<ProjectPhoto>();

        public Contractor Contractor { get; set; }
    }
}