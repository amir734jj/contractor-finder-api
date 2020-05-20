using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Interfaces;
using Models.Entities.Users;
using Models.Interfaces;

namespace Logic.Abstracts
{
    public abstract class BasicCrudBoundLogicAbstract<TParent, TItem> : IBasicCrudBoundLogic<TItem>
        where TItem : IEntity
        where TParent: IEntity
    {
        public async Task<IBasicCrudLogic<TItem>> For(User user)
        {
            var userEntity = await UserLogic().Get(user.Id);

            var (parent, list) = ResolveSources(userEntity);

            return new BasicListCrud(list, StateChangeSignal(parent));
        }

        private Func<Task> StateChangeSignal(TParent parent)
        {
            return async () => await ParentLogic().Update(parent.Id, parent);
        }

        protected abstract (TParent, List<TItem>) ResolveSources(User user);

        protected abstract IBasicCrudLogic<User> UserLogic();
        
        protected abstract IBasicCrudLogic<TParent> ParentLogic();

        private class BasicListCrud : IBasicCrudLogic<TItem>
        {
            private readonly List<TItem> _source;
            
            private readonly Func<Task> _stateChangeSignal;

            public BasicListCrud(List<TItem> source, Func<Task> stateChangeSignal)
            {
                _source = source;
                _stateChangeSignal = stateChangeSignal;
            }

            public async Task<IEnumerable<TItem>> GetAll()
            {
                return _source;
            }

            public async Task<TItem> Get(Guid id)
            {
                return _source.FirstOrDefault(x => x.Id == id);
            }

            public async Task<TItem> Save(TItem instance)
            {
                _source.Add(instance);

                await _stateChangeSignal();

                return instance;
            }

            public async Task<TItem> Delete(Guid id)
            {
                var item = await Get(id);

                if (item != null)
                {
                    _source.Remove(item);

                    await _stateChangeSignal();
                }

                return item;
            }

            public async Task<TItem> Update(Guid id, TItem dto)
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