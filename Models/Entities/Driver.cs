using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Models.Enums;
using Models.Interfaces;
using HashCode = Invio.Hashing.HashCode;

namespace Models.Entities
{
    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public class Driver : IPerson
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name="Email")]
        public string Email { get; set; }
        
        [Display(Name="Phone")]
        public string Phone { get; set; }
        
        [Display(Name="Fullname")]
        public string Fullname { get; set; }
        
        public string DisplayId { get; set; }
        
        [Display(Name = "Capacity")]
        
        [Range(1, 7)]
        public int Capacity { get; set; } = 1;
        
        [Display(Name="Require Navigator")]
        public bool RequireNavigator { get; set; } = true;
        
        [Display(Name="Navigator fullname")]
        public string Navigator { get; set; }
        
        [Display(Name="Role")]
        public RolesEnum Role { get; set; }
        
        public HashSet<Student> Students { get; set; } = new HashSet<Student>();
        
        /// <summary>
        /// Optional
        /// </summary>
        public int? HostRefId { get; set; }

        [ForeignKey("HostRefId")]
        public Host Host { get; set; }
        
        public bool IsPresent { get; set; }
        
        [Display(Name = "Have a child seat available")]
        public bool HaveChildSeat { get; set; }
        
        /// <summary>
        /// Indicates the year in which driver attended the tour
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Override generate hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Math.Abs(HashCode.From(
                HashCode.From(Id),
                HashCode.FromSet(Email),
                HashCode.FromSet(Phone),
                HashCode.FromSet(Fullname)
            ));
        }
    }
}