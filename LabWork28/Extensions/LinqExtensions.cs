using System.Linq.Expressions;

namespace LabWork28.Extensions;

public static class LinqExtensions
{
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string columnName, bool desc = false)
    {
        var entityType = typeof(T);
        var propertyInfo = entityType.GetProperty(columnName);
        var parameter = Expression.Parameter(entityType, "x");
        var property = Expression.Property(parameter, propertyInfo);
        var lambda = Expression.Lambda(property, parameter);
        var methodName = desc ? "OrderByDescending" : "OrderBy";
        var expression = Expression.Call(typeof(Queryable), methodName,
            new Type[] { entityType, propertyInfo.PropertyType },
            query.Expression, Expression.Quote(lambda));
        return query.Provider.CreateQuery<T>(expression);
    }
    
    public static IQueryable<T> ThenBy<T>(this IQueryable<T> query, string columnName,
        bool desc = false) 
    {
        var entityType = typeof(T);
        var propertyInfo = entityType.GetProperty(columnName);
        var parameter = Expression.Parameter(entityType, "x");
        var property = Expression.Property(parameter, propertyInfo);
        var lambda = Expression.Lambda(property, parameter);
        var methodName = desc ? "ThenByDescending" : "ThenBy";
        var expression = Expression.Call(typeof(Queryable), methodName,
            new Type[] { entityType, propertyInfo.PropertyType },
            query.Expression, Expression.Quote(lambda));
        return query.Provider.CreateQuery<T>(expression);
    }
}