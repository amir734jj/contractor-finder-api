using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Interfaces
{
    public interface IEntity
    {
        [Key]
        Guid Id { get; set; }
    }
}