using System;

namespace Models.Entities.Common
{
    public class Photo : Entity
    {
        public Guid Key { get; set; }
        
        public string Name { get; set; }
    }
}