using Models.Interfaces;

namespace Models.ViewModels
{
    /// <summary>
    /// Student-Driver mapping view model for new map
    /// </summary>
    public class NewStudentDriverMappingViewModel : IViewModel
    {
        public int StudentId { get; set; }
        
        public int DriverId { get; set; }
    }
}