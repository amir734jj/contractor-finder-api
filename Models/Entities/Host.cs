using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Interfaces;

namespace Models.Entities
{
    public class Host : IPerson
    {
        [Key]
        public int Id { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public string Fullname { get; set; }
        
        public string Address { get; set; }
        
        public HashSet<Driver> Drivers { get; set; } = new HashSet<Driver>();
        
        /// <summary>
        /// Indicates the year in which host attended the tour
        /// </summary>
        public int Year { get; set; }
    }
}