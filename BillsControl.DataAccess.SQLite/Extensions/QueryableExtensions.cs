namespace BillsControl.DataAccess.SQLite.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> ApplyPagination<TEntity>(
        this IQueryable<TEntity> query,
        int? page, 
        int? pageSize)
    {
        if (page.HasValue && pageSize.HasValue)
        {
            return query
                .Skip(pageSize.Value * (page.Value - 1))
                .Take(pageSize.Value);
        }
        return query;
    }
}