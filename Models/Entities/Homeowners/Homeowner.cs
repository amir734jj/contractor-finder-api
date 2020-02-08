using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Models.Entities.Projects;
using Models.Entities.Users;
using Models.Interfaces;

namespace Models.Entities.Homeowners
{
    public class Homeowner : IEntity
    {
        public Guid Id { get; set; }

        public string Address { get; set; }
        
        public List<HomeownerProject> Projects { get; set; } = new List<HomeownerProject>();
        
        [JsonIgnore]
        public User UserRef { get; set; }
    }
}