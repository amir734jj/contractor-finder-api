using System;
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
    public class User : IdentityUser<Guid>, IPerson, IEntity
    {
        public RoleEnum Role { get; set; }

        public string Description { get; set; }
        
        public string Name { get; set; }

        /// <summary>
        /// Keys to lookup from S3
        /// 
        /// Used nullable Guid to avoid 0000-0000-... being as the default value
        /// </summary>
        public Photo PhotoRef { get; set; }
        
        public Guid? PhotoKey { get; set; }

        public Contractor ContractorRef { get; set; }

        public Guid? ContractorKey { get; set; }

        public Homeowner HomeownerRef { get; set; }

        public Guid? HomeownerKey { get; set; }

        public InternalUser InternalUserRef { get; set; }

        public Guid? InternalUserKey { get; set; }
    }
}