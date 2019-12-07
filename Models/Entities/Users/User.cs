using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Common;
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

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        /// <summary>
        /// Keys to lookup from S3
        /// </summary>
        [JsonIgnore]
        public Guid ProfilePhotoKey { get; set; }

        [NotMapped]
        public ProfilePhoto ProfilePhoto { get; set; }

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