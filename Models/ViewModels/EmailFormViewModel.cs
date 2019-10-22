using Models.Interfaces;

namespace Models.ViewModels
{
    /// <summary>
    /// AdHoc email form
    /// </summary>
    public class EmailFormViewModel : IViewModel
    {
        public bool Admin { get; set; }

        public bool Users { get; set; }
        
        public bool Students { get; set; }
        
        public bool Drivers { get; set; }
        
        public bool Hosts { get; set; }
        
        public string Subject { get; set; }
        
        public string Message { get; set; }

        public bool Status { get; set; } = true;
    }
}