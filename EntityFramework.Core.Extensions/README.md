# EntityFramework.Core.Extensions

Small extensions to EntityFramework.Core
- `ToSql<TEntity>(this IQueryable<TEntity>, DbContext)`
- `TEntity FirstOrDefaultCache<TEntity>(this DbSet<TEntity>, Expression<Func<TEntity, bool>>)`
- `Task<TEntity> FirstOrDefaultCacheAsync<TEntity>(this DbSet<TEntity>, Expression<Func<TEntity, bool>>)`
