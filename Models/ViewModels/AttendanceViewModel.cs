using Models.Interfaces;

namespace Models.ViewModels
{
    public class AttendanceViewModel : IViewModel
    {
        public int Id { get; set; }
        
        public bool Attendance { get; set; }
    }
}