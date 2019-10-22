using System.Linq;
using System.Threading.Tasks;
using Logic.Interfaces;

namespace Logic.Extensions
{
    public static class BasicCrudLogicExtensions
    {
        /// <summary>
        /// Returns the first or default match given hashcode
        /// </summary>
        /// <param name="basicCrudLogic"></param>
        /// <param name="hashcode"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> GetByHashcode<T>(this IBasicCrudLogic<T> basicCrudLogic, int hashcode)
        {
            return (await basicCrudLogic.GetAll()).FirstOrDefault(x => x.GetHashCode() == hashcode);
        }
    }
}