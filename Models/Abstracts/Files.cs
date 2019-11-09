using System;
using System.ComponentModel.DataAnnotations;
using Models.Interfaces;

namespace Models.Abstracts
{
    public abstract class AbstractFileEntity : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string MimeType { get; set; }

        public byte[] Bytes { get; set; }
    }
}