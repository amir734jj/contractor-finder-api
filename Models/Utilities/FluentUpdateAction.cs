using System;

namespace Models.Utilities
{
    public static class FluentUpdateAction
    {
        public static T FluentUpdate<T>(T source, Action<T> update)
        {
            // Call update on source
            update(source);

            return source;
        }
    }
}