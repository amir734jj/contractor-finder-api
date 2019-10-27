using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dal.Interfaces;
using Logic.Interfaces;

namespace Logic.Abstracts
{
    public abstract class BasicCrudLogicAbstract<T> : IBasicCrudLogic<T>
    {
        /// <summary>
        /// Returns instance of basic DAL
        /// </summary>
        /// <returns></returns>
        protected abstract IBasicCrudDal<T> GetBasicCrudDal();

        /// <summary>
        /// Call forwarding
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await GetBasicCrudDal().GetAll();
        }

        /// <summary>
        /// Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> Get(Guid id)
        {
            return await GetBasicCrudDal().Get(id);
        }

        /// <summary>
        /// Call forwarding
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual async Task<T> Save(T instance)
        {
            return await GetBasicCrudDal().Save(instance);
        }

        /// <summary>
        /// Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> Delete(Guid id)
        {
            return await GetBasicCrudDal().Delete(id);
        }

        /// <summary>
        /// Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedInstance"></param>
        /// <returns></returns>
        public virtual async Task<T> Update(Guid id, T updatedInstance)
        {
            return await GetBasicCrudDal().Update(id, updatedInstance);
        }

        /// <summary>
        /// Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyAction"></param>
        /// <returns></returns>
        public virtual async Task<T> Update(Guid id, Action<T> modifyAction)
        {
            return await GetBasicCrudDal().Update(id, modifyAction);
        }
    }
}