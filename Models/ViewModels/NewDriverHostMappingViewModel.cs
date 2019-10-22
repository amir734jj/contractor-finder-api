using Models.Interfaces;

namespace Models.ViewModels
{
    /// <summary>
    /// Driver-Host mapping view model for new map
    /// </summary>
    public class NewDriverHostMappingViewModel : IViewModel
    {
        public int DriverId { get; set; }
        
        public int HostId { get; set; }
    }
}