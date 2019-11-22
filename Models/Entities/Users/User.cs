using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Common;
using Models.Entities.Contractors;
using Models.Entities.Homeowners;
using Models.Entities.Internals;
using Models.Interfaces;

namespace Models.Entities.Users
{
    public class User : IdentityUser<Guid>, IPerson
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public ProfilePhoto ProfilePhoto { get; set; }


        [JsonIgnore] public Contractor ContractorRef { get; set; }

        public Guid? ContractorKey { get; set; }


        [JsonIgnore] public Homeowner HomeownerRef { get; set; }

        public Guid? HomeownerKey { get; set; }

        [JsonIgnore] public InternalUser InternalUserRef { get; set; }

        public Guid? InternalUserKey { get; set; }
    }
}