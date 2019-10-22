using System.Threading.Tasks;
using Models.Enums;
using Models.ViewModels;

namespace Logic.Interfaces
{
    public interface IEmailUtilityLogic
    {
        Task<bool> HandleEventEmail(EmailEventViewModel emailEventViewModel);
        
        Task<bool> HandleAdHocEmail(EmailFormViewModel emailFormViewModel);

        bool HandleEmailCheckIn(EntitiesEnum entitiesEnum, int id, bool present);
    }
}