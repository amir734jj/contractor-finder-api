using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Models.Enums;

namespace Models.Entities.Users
{
    public class UserRole : IdentityRole<Guid>
    {
        /// <summary>
        ///     Website admin
        /// </summary>
        [JsonIgnore]
        public uint Role { get; set; }

        /// <summary>
        ///     Roles where user belongs to
        /// </summary>
        [NotMapped]
        public IEnumerable<RoleEnum> Roles
        {
            get => Enum.GetValues(typeof(RoleEnum))
                .Cast<RoleEnum>()
                .Where(x => ((uint) x & Role) != 0);
            set => Role = value.Aggregate((uint) 0, (x, acc) => x | (uint) acc);
        }
    }
}