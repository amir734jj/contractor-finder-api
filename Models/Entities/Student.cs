using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Models.Interfaces;
using HashCode = Invio.Hashing.HashCode;

namespace Models.Entities
{
    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public class Student : IPerson
    {
        [Key]
        public int Id { get; set; }
        
        public string Fullname { get; set; }
        
        public string Major { get; set; }
        
        public string University { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public string Country { get; set; }

        [Display(Name = "Tell us some of your interests")]
        public string Interests { get; set; }

        [Display(Name = "Family members joining you (not including yourself)")]
        public int FamilySize { get; set; } = 1;
        
        public string DisplayId { get; set; }
        
        [Display(Name="Need a car seat?")]
        public bool NeedCarSeat { get; set; }
        
        [Display(Name="Halal or Kosher food")]
        public bool KosherFood { get; set; }
        
        public bool IsPresent { get; set; }
        
        /// <summary>
        /// Optional
        /// </summary>
        public int? DriverRefId { get; set; }

        [ForeignKey("DriverRefId")]
        public Driver Driver { get; set; }
        
        [Display(Name = "Registering as a family?")]
        public bool IsFamily { get; set; }

        /// <summary>
        /// Indicates the year in which student attended the tour
        /// </summary>
        public int Year { get; set; }
        
        /// <summary>
        /// List of Event Student Relationships
        /// </summary>
        public List<EventStudentRelationship> Events { get; set; } = new List<EventStudentRelationship>();
        
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