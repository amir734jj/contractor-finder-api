using Models.Abstracts;
using Models.Entities.Homeowners;

namespace Models.Entities.Projects
{
    public class HomeownerProject : AbstractProject
    {
        public Homeowner Homeowner { get; set; }
    }
}