using System;
using System.Text.Json.Serialization;
using Models.Entities.Users;
using Models.Interfaces;

namespace Models.Entities.Internals
{
    public class InternalUser : IEntity
    {
        public Guid Id { get; set; }
        
        [JsonIgnore]
        public User UserRef { get; set; }
    }
}