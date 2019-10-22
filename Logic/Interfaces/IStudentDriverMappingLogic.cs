using System.Threading.Tasks;
using Models.ViewModels;

namespace Logic.Interfaces
{
    public interface IStudentDriverMappingLogic
    {
        Task<bool> MapStudentToDriver(NewStudentDriverMappingViewModel newStudentDriverMappingViewModel);
        
        Task<bool> UnMapStudentToDriver(NewStudentDriverMappingViewModel newStudentDriverMappingViewModel);

        Task<StudentDriverMappingViewModel> MappingStatus();
        
        Task<bool> EmailMappings();
    }
}