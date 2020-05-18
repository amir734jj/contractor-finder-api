using System;
using Models.Interfaces;

namespace Models.Entities.Common
{
    public class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}