using System;
using Models.Entities.Users;
using Models.Interfaces;
using Newtonsoft.Json;

namespace Models.Entities.Internals
{
    public class InternalUser : IEntity
    {
        public Guid Id { get; set; }
        
        [JsonIgnore]
        public User UserRef { get; set; }
    }
}