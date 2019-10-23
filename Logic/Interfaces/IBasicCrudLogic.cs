using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IBasicCrudLogic<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(Guid id);

        Task<T> Save(T instance);
        
        Task<T> Delete(Guid id);

        Task<T> Update(Guid id, T updatedInstance);
        
        Task<T> Update(Guid id, Action<T> modifyAction);
    }
}