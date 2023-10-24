namespace CandyBackend.Core;

public interface ICandyService
{
    List<Candy> GetCandies();

    Candy GetCandy(long candyId);

    Candy Save(Candy candy);

    Candy Update(long candyId, Candy candy);
}