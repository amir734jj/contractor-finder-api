using System.Collections.Generic;
using Models.Entities;

namespace Models.ViewModels
{
    public class EventManagementViewModel
    {
        public Event Event { get; set; }
        
        public IEnumerable<Student> AvailableStudents { get; set; }
    }
}