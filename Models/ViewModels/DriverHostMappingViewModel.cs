using System.Collections.Generic;
using Models.Entities;
using Models.Interfaces;

namespace Models.ViewModels
{
    /// <summary>
    /// Driver-Host mapping all
    /// </summary>
    public class DriverHostMappingViewModel : IViewModel
    {
        public IEnumerable<Driver> AvailableDrivers { get; set; }
        
        public IEnumerable<Host> AvailableHosts { get; set; }
        
        public IEnumerable<Driver> MappedDrivers { get; set; }
        
        public IEnumerable<Host> MappedHosts { get; set; }
    }
}