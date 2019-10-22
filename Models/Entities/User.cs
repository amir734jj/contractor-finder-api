using System.ComponentModel.DataAnnotations;
using Models.Enums;
using Models.Interfaces;

namespace Models.Entities
{
    public class User : IPerson
    {
        [Key]
        public int Id { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public string Fullname { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        [Display(Name = "User Role")]
        public UserRoleEnum UserRoleEnum { get; set; }
    }
}