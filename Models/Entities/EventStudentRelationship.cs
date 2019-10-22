using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class EventStudentRelationship
    {
        [Key]
        public int Id { get; set; }
        
        public int StudentId { get; set; }
        
        public Student Student { get; set; }

        public int EventId { get; set; }
        
        public Event Event { get; set; }
    }
}