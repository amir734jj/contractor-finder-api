using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Interfaces;
using Models.Entities.Users;
using Models.Interfaces;

namespace Logic.Abstracts
{
    public abstract class BasicCrudBoundLogicAbstract<T> : IBasicCrudBoundLogic<T> where T : IEntity
    {
        public async Task<IBasicCrudLogic<T>> For(User user)
        {
            var entity = await UserLogic().Get(user.Id);

            return new BasicListCrud(ResolveSource(entity), StateChangeSignal(entity));
        }

        private Func<Task> StateChangeSignal(User user)
        {
            return async () => await UserLogic().Update(user.Id, user);
        }

        protected abstract List<T> ResolveSource(User user);

        protected abstract IBasicCrudLogic<User> UserLogic();

        private class BasicListCrud : IBasicCrudLogic<T>
        {
            private readonly List<T> _source;
            
            private readonly Func<Task> _stateChangeSignal;

            public BasicListCrud(List<T> source, Func<Task> stateChangeSignal)
            {
                _source = source;
                _stateChangeSignal = stateChangeSignal;
            }

            public async Task<IEnumerable<T>> GetAll()
            {
                return _source;
            }

            public async Task<T> Get(Guid id)
            {
                return _source.FirstOrDefault(x => x.Id == id);
            }

            public async Task<T> Save(T instance)
            {
                _source.Add(instance);

                await _stateChangeSignal();

                return instance;
            }

            public async Task<T> Delete(Guid id)
            {
                var item = await Get(id);

                if (item != null)
                {
                    _source.Remove(item);

                    await _stateChangeSignal();
                }

                return item;
            }

            public async Task<T> Update(Guid id, T dto)
            {
                var item = await Get(id);

                if (item != null)
                {
                    _source[_source.FindIndex(x => x.Id == id)] = dto;

                    await _stateChangeSignal();
                }

                return item;
            }
        }
    }
}