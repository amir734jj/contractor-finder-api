﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Models.Entities.Projects;
using Models.Entities.Users;
using Models.Interfaces;

namespace Models.Entities.Contractors
{
    public class Contractor : IEntity
    {
        public Guid Id { get; set; }

        public List<ShowcaseProject> ShowcaseProjects { get; set; }
        
        public List<HomeownerProject> HomeownerProjects { get; set; }
        
        [JsonIgnore]
        public User UserRef { get; set; }
    }
}