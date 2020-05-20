using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities.Users;

namespace Logic.Interfaces
{
    public interface IBasicCrudBoundLogic<T>
    {
        public Task<IBasicCrudLogic<T>> For(User user);
    }
    
    public interface IBasicCrudLogic<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(Guid id);

        Task<T> Save(T instance);
        
        Task<T> Delete(Guid id);

        Task<T> Update(Guid id, T dto);
    }
}