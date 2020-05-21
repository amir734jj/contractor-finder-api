using System.Collections.Generic;
using Models.Entities.Common;
using Models.Entities.Contractors;

namespace Models.Entities.Projects
{
    public class ShowcaseProject : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<DescriptivePhoto> ProjectPhotos { get; set; } = new List<DescriptivePhoto>();

        public Contractor Contractor { get; set; }
    }
}