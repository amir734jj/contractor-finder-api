using System.Linq;
using Models.Interfaces;

namespace Logic.Utilities
{
    public static class DisplayIdUtility
    {
        /// <summary>
        /// Generates a display Id
        /// </summary>
        /// <param name="person"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GenerateDisplayId(IPerson person, int count)
        {            
            return (string.Join(string.Empty, person.Fullname.Split(" ")
                       .Select(x => x.Trim())
                       .Where(x => !string.IsNullOrWhiteSpace(x))
                       .Select(x => x.Substring(0, 1))) + "-" + ++count).ToUpper();
        }
    }
}