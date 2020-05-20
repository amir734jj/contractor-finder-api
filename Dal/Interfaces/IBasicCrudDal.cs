using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Interfaces;

namespace Dal.Interfaces
{
    public interface IBasicCrudDal<T> where T: IEntity
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(Guid id);

        Task<T> Save(T instance);
        
        Task<T> Delete(Guid id);

        Task<T> Update(Guid id, T dto);
    }
}