using System;
using System.Collections.Generic;
using Models.Entities.Common;
using Models.Entities.Contractors;
using Models.Interfaces;

namespace Models.Abstracts
{
    public class AbstractProject : IEntity
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public List<ProjectPhoto> ProjectPhotos { get; set; }
        
        public Contractor Contractor { get; set; }
    }
}