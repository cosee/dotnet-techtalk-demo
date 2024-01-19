using System.Linq.Expressions;
using CandyBackend.Core;
using CandyBackend.Repository.Candies;

namespace CandyBackend.Repository;

public static class PagingAndSortingExtension
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> queryable, int? limit, int? offset)
    {
        if (offset != null && limit != null)
        {
            return queryable
                .Skip(offset.Value)
                .Take(limit.Value);
        }

        return queryable;
    }

    public static Expression<Func<CandyEntity, dynamic?>> GetComparatorForCandySortBy(CandySortBy sortBy)
    {
        return sortBy switch
        {
            CandySortBy.Name => c => c.Name,
            CandySortBy.Price => c => c.Price,
            _ => throw new ArgumentOutOfRangeException(nameof(sortBy), sortBy.ToString())
        };
    }

    public static IQueryable<CandyEntity> ApplySorting(this IQueryable<CandyEntity> candies, CandySortBy? sortBy, SortDir? sortDir)
    {
        if (sortBy == null || sortDir == null)
        {
            return candies;
        }

        var comparatorForSortBy = GetComparatorForCandySortBy(sortBy.Value);
        candies = sortDir == SortDir.Asc
            ? candies.OrderBy(comparatorForSortBy)
            : candies.OrderByDescending(comparatorForSortBy);

        return candies;
    }
}