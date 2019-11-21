using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Entities.Projects;
using Models.Entities.Users;
using Models.Interfaces;

namespace Models.Entities.Homeowners
{
    public class Homeowner : IEntity
    {
        public Guid Id { get; set; }

        public string Address { get; set; }
        
        public List<HomeownerProject> Projects { get; set; }
        
        public User UserRef { get; set; }
    }
}