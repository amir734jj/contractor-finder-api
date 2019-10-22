using System.Collections.Generic;
using Models.Interfaces;

namespace Models.ViewModels
{
    public class EmailEventViewModel : IViewModel
    {
        public IEnumerable<string> Emails { get; set; }
        
        public string Subject { get; set; }
        
        public string Body { get; set; }
    }
}