using System;
using System.Text.Json.Serialization;
using Models.Abstracts;

namespace Models.Entities.ContractorEntities
{
    public class ContractorProfilePhoto : AbstractFileEntity
    {
        public Guid ContractorId { get; set; }

        [JsonIgnore]
        public Contractor Contractor { get; set; }
    }
}