using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Contractors;
using Models.Entities.Homeowners;
using Models.Entities.Internals;
using Models.Enums;
using Models.Interfaces;

namespace Models.Entities.Users
{
    public class User : IdentityUser<Guid>, IPerson
    {
        public RoleEnum Role { get; set; }

        public string Description { get; set; }
        
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        /// <summary>
        /// Keys to lookup from S3
        /// 
        /// Used nullable Guid to avoid 0000-0000-... being as the default value
        /// </summary>
        public Guid? Photo { get; set; }

        [JsonIgnore]
        public Contractor ContractorRef { get; set; }

        public Guid? ContractorKey { get; set; }
        
        [JsonIgnore]
        public Homeowner HomeownerRef { get; set; }

        public Guid? HomeownerKey { get; set; }

        [JsonIgnore]
        public InternalUser InternalUserRef { get; set; }

        public Guid? InternalUserKey { get; set; }
    }
}