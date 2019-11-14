using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Interfaces;

namespace Models.Abstracts
{
    public abstract class AbstractFileEntity : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string MimeType { get; set; }

        [Column(TypeName = "text")]
        [MaxLength] 
        public string Base64 { get; set; }
    }
}