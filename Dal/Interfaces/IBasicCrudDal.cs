using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBasicCrudDal<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(Guid id);

        Task<T> Save(T instance);
        
        Task<T> Delete(Guid id);

        Task<T> Update(Guid id, T dto);
        
        Task<T> Update(Guid id, Action<T> modifyAction);
    }
}