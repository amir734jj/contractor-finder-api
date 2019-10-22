using System.Collections.Generic;
using Models.Entities;
using Models.Interfaces;

namespace Models.ViewModels
{
    /// <summary>
    /// Student-Driver mapping all
    /// </summary>
    public class StudentDriverMappingViewModel : IViewModel
    {
        public List<KeyValuePair<Driver, bool>> AvailableDrivers { get; set; }
        
        public IEnumerable<Student> AvailableStudents { get; set; }
        
        public IEnumerable<Driver> MappedDrivers { get; set; }
        
        public IEnumerable<Student> MappedStudents { get; set; }
    }
}