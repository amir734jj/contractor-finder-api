using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Abstracts;
using Models.Interfaces;

namespace Models.Entities
{
    public class Contractor : IPerson
    {
        [Key]
        public Guid Id { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Firstname { get; set; }
 
        public string Lastname { get; set; }

        public ContractorProfilePhoto ProfilePhoto { get; set; }
    }

    [Table("ProfilePhotos")]
    public class ContractorProfilePhoto : AbstractFileEntity
    {
        public Guid ContractorId { get; set; }
        
        public Contractor Contractor { get; set; }
    }
}