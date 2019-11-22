using System;
using System.Collections.Generic;
using Models.Entities.Projects;
using Models.Entities.Users;
using Models.Interfaces;
using Newtonsoft.Json;

namespace Models.Entities.Homeowners
{
    public class Homeowner : IEntity
    {
        public Guid Id { get; set; }

        public string Address { get; set; }
        
        public List<HomeownerProject> Projects { get; set; }
        
        [JsonIgnore]
        public User UserRef { get; set; }
    }
}