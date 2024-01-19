namespace CandyBackend.Core;

public interface ICandyService
{
    List<Candy> GetCandies(int limit, int offset, CandySortBy sortBy, SortDir sortDir);

    Candy GetCandy(long candyId);

    Candy Save(Candy candy);

    Candy Update(long candyId, Candy candy);
}