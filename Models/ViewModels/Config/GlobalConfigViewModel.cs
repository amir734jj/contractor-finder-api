using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels.Config
{
    public class GlobalConfigViewModel
    {
        public IEnumerable<int> Years { get; set; }
        
        public int UpdatedYear { get; set; }
        
        [Display(Name = "AD-HOC Event Feature")]
        public bool EventFeature { get; set; }

        [Display(Name = "Email Test Mode")]
        public bool EmailTestMode { get; set; }
    }
}