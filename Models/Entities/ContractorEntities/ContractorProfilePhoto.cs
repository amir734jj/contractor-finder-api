using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Abstracts;

namespace Models.Entities.ContractorEntities
{
    [Table("ProfilePhotos")]
    public class ContractorProfilePhoto : AbstractFileEntity
    {
        public Guid ContractorId { get; set; }
        
        public Contractor Contractor { get; set; }
    }
}