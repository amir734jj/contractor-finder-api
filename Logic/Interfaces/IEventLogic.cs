using System.Threading.Tasks;
using Models.Entities;
using Models.ViewModels;

namespace Logic.Interfaces
{
    public interface IEventLogic : IBasicCrudLogic<Event>
    {
        Task<EventManagementViewModel> GetEventInfo(int id);
        
        Task<bool> MapStudent(int eventId, int studentId);
        
        Task<bool> UnMapStudent(int eventId, int studentId);
    }
}