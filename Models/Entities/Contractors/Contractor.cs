﻿using System.Collections.Generic;
using Models.Entities.Projects;
using Models.Entities.Users;
using Models.Enums;
using Models.Interfaces;

namespace Models.Entities.Contractors
{
    public class Contractor : User, IEntity
    {
        public List<ShowcaseProject> ShowcaseProjects { get; set; }
        
        public List<HomeownerProject> HomeownerProjects { get; set; }
        public override RoleEnum ResolveRole()
        {
            return RoleEnum.Contractor;
        }
    }
}