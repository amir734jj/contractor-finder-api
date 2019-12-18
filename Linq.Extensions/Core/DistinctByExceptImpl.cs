using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Linq.Extensions
{
    public static class DistinctByExceptImpl
    {
        private sealed class ResolveMembersVisistor : ExpressionVisitor
        {
            public string Member { get; private set; }
            
            public ResolveMembersVisistor(Expression expression)
            {
                Visit(expression);
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                Member = Member == null
                    ? node.Member.Name
                    : throw new Exception("Nested member access is not allowed!");

                return node;
            }
        }

        /// <summary>
        ///     DistinctBy Except
        /// </summary>
        /// <param name="source"></param>
        /// <param name="exceptKeySelectors"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> DistinctByExcept<T>(this IEnumerable<T> source, params Expression<Func<T, object>>[] exceptKeySelectors)
        {
            // Initialize the table
            var type = typeof(T);

            var keySelectors = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(x => x.Name)
                .Except(exceptKeySelectors.Select(x => new ResolveMembersVisistor(x).Member))
                .Select(x => (arg: Expression.Parameter(type), name: x))
                .Select(x => (arg: x.arg, access: Expression.Property(x.arg, x.name)))
                .Select(x => Expression.Lambda<Func<T, object>>(x.access, x.arg).Compile())
                .ToArray();

            return source.DistinctBy(keySelectors);
        }
    }
}