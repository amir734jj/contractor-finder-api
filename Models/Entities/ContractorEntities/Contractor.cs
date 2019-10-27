using System;
using System.ComponentModel.DataAnnotations;
using Models.Interfaces;

namespace Models.Entities.ContractorEntities
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
}