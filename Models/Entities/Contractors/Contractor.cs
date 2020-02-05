using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities.Projects;
using Models.Entities.Users;
using Models.Enums;
using Models.Interfaces;

namespace Models.Entities.Contractors
{
    public class Contractor : IEntity
    {
        public Guid Id { get; set; }

        public List<ShowcaseProject> ShowcaseProjects { get; set; }

        public List<ProjectMilestone> HomeownerProjects { get; set; }

        public User UserRef { get; set; }

        [Column(TypeName = "jsonb")]
        public List<ContractorSpecialityEnum> Speciality { get; set; }
    }
}