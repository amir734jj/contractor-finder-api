using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Core
{
    /// <summary>
    /// IQueryable extensions
    /// </summary>
    public static class QueryableExtension
    {
        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            var enumerator = query.Provider.Execute<IEnumerable<TEntity>>(query.Expression).GetEnumerator();
            var enumeratorType = enumerator.GetType();
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
            var selectExpressionFieldName = "_selectExpression";
            var querySqlGeneratorFactoryFieldName = "_querySqlGeneratorFactory";
            var selectFieldInfo = enumeratorType.GetField(selectExpressionFieldName, bindingFlags);
            var sqlGeneratorFieldInfo = enumeratorType.GetField(querySqlGeneratorFactoryFieldName, bindingFlags);
            if (selectFieldInfo == null || sqlGeneratorFieldInfo == null)
                throw new InvalidOperationException($"Cannot find field {(selectFieldInfo == null ? selectExpressionFieldName : querySqlGeneratorFactoryFieldName) } on type {enumeratorType.Name}");
            var selectExpression = selectFieldInfo.GetValue(enumerator) as SelectExpression;
            var factory = sqlGeneratorFieldInfo.GetValue(enumerator) as IQuerySqlGeneratorFactory;
            if (selectExpression == null || factory == null)
                throw new InvalidOperationException($"Could not get {(nameof(IQuerySqlGeneratorFactory)) }");
            var sqlGenerator = factory.Create();
            var command = sqlGenerator.GetCommand(selectExpression);
            var sql = command.CommandText;
            return sql;
        }
        
        /// <summary>
        /// Converts P-SQL (ADO.NET) string given an IQueryable&lt;TEntity&gt; and DbContext
        /// </summary>
        /// <param name="query"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static (string, IEnumerable<SqlParameter>) ToParametrizedSql<TEntity>(this IQueryable<TEntity> query)
        {
            var enumerator = query.Provider
                .Execute<IEnumerable<TEntity>>(query.Expression)
                .GetEnumerator();
            const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
            var enumeratorType = enumerator.GetType();
            var selectFieldInfo = enumeratorType.GetField("_selectExpression", bindingFlags)
                                  ?? throw new InvalidOperationException(
                                      $"cannot find field _selectExpression on type {enumeratorType.Name}");
            var sqlGeneratorFieldInfo = enumeratorType.GetField("_querySqlGeneratorFactory", bindingFlags)
                                        ?? throw new InvalidOperationException(
                                            $"cannot find field _querySqlGeneratorFactory on type {enumeratorType.Name}");
            var queryContextFieldInfo = enumeratorType.GetField("_relationalQueryContext", bindingFlags)
                                        ?? throw new InvalidOperationException(
                                            $"cannot find field _relationalQueryContext on type {enumeratorType.Name}");
            var selectExpression = selectFieldInfo.GetValue(enumerator) as SelectExpression
                                   ?? throw new InvalidOperationException($"could not get SelectExpression");
            var factory = sqlGeneratorFieldInfo.GetValue(enumerator) as IQuerySqlGeneratorFactory ??
                          throw new InvalidOperationException($"could not get SqlServerQuerySqlGeneratorFactory");
            var queryContext = queryContextFieldInfo.GetValue(enumerator) as RelationalQueryContext
                               ?? throw new InvalidOperationException($"could not get RelationalQueryContext");
            var sqlGenerator = factory.Create();
            var command = sqlGenerator.GetCommand(selectExpression);
            var parameters = new List<SqlParameter>();
            if (queryContext.ParameterValues.Count > 0)
                parameters = queryContext.ParameterValues.Select(a => new SqlParameter("@" + a.Key, a.Value)).ToList();
            var sql = command.CommandText;
            return (sql, parameters);
        }
    }
}