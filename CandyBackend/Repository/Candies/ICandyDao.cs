using CandyBackend.Core;

namespace CandyBackend.Repository.Candies;

public interface ICandyDao
{
    Candy Save(Candy candy);

    Candy Update(long candyId, Candy candy);

    Candy GetCandy(long candyId);

    List<Candy> GetCandies(int limit, int offset, CandySortBy sortBy, SortDir sortDir);

    List<Candy> FindCandiesById(ICollection<long> candyIds);
}