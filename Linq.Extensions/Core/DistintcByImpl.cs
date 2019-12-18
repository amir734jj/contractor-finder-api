using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq.Extensions
{
    public static class DistintcByImpl
    {
        /// <summary>
        ///     DistinctBy IEnumerable
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keySelectors"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> source, params Func<T, object>[] keySelectors)
        {
            // Initialize the table
            var seenKeysTable = keySelectors.ToDictionary(x => x, x => new HashSet<object>());

            // Loop through each element in source
            foreach (var element in source)
            {
                // Initialize the flag to true
                var flag = true;

                // Loop through each keySelector a
                foreach (var (keySelector, hashSet) in seenKeysTable)
                {                    
                    // If all conditions are true
                    flag = flag && hashSet.Add(keySelector(element));
                }

                // If no duplicate key was added to table, then yield the list element
                if (flag)
                {
                    yield return element;
                }
            }
        }
    }
}