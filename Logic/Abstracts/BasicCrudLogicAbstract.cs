using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        /// AutoMapper instance
        /// </summary>
        /// <returns></returns>
        protected abstract IMapper Mapper();

        /// <summary>
        /// Call forwarding
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return Map(await GetBasicCrudDal().GetAll());
        }

        /// <summary>
        /// Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> Get(Guid id)
        {
            return Map(await GetBasicCrudDal().Get(id));
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

        /// <summary>
        /// Map for IEnumerable
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private IEnumerable<T> Map(IEnumerable<T> items)
        {
            return items.Select(Map);
        }

        /// <summary>
        /// Method that derives custom properties upon GET
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual T Map(T item)
        {
            return Mapper().Map<T>(item);
        }
    }
}