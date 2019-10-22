using System.Threading.Tasks;
using Models.ViewModels;

namespace Logic.Interfaces
{
    public interface IDriverHostMappingLogic
    {
        Task<bool> MapDriverToHost(NewDriverHostMappingViewModel newDriverHostMappingViewModel);
        
        Task<bool> UnMapDriverToHost(NewDriverHostMappingViewModel newDriverHostMappingViewModel);

        Task<DriverHostMappingViewModel> MappingStatus();

        Task<bool> EmailMappings();
    }
}