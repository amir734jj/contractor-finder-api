using System;
using System.ComponentModel.DataAnnotations;
using Models.Entities.Users;
using Models.Interfaces;

namespace Models.Entities.Internals
{
    public class InternalUser : IEntity
    {
        public Guid Id { get; set; }
        
        public User UserRef { get; set; }
    }
}