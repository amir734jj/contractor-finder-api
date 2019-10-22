using System.Threading.Tasks;
using Models.Entities;

namespace Logic.Interfaces
{
    public interface IRegistrationLogic
    {
        Task<bool> RegisterDriver(Driver driver);

        Task<bool> RegisterStudent(Student student);
        
        Task<bool> RegisterHost(Host host);

        Task<bool> RegisterEvent(Event @event);
    }
}