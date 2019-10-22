using System.Threading.Tasks;
using Models.ViewModels.Config;

namespace Logic.Interfaces
{
    public interface IConfigLogic
    {
        Task<GlobalConfigViewModel> ResolveGlobalConfig();

        Task SetGlobalConfig(GlobalConfigViewModel globalConfigViewModel);
    }
}