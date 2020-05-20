using System.Threading.Tasks;
using Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Users;
using Models.Interfaces;

namespace Api.Abstracts
{
    public abstract class BasicCrudBoundController<T> : BasicCrudController<T> where T: IEntity
    {
        [NonAction]
        protected abstract IBasicCrudBoundLogic<T> BasicCrudUserBoundLogic();

        [NonAction]
        protected abstract UserManager<User> UserManager();

        protected override async Task<IBasicCrudLogic<T>> BasicCrudLogic()
        {
            var user = await UserManager().FindByEmailAsync(User.Identity.Name);
            
            return await BasicCrudUserBoundLogic().For(user);
        }
    }
}