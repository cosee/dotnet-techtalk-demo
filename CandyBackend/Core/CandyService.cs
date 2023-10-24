using CandyBackend.Repository.Candies;

namespace CandyBackend.Core;

public class CandyService : ICandyService
{
    private readonly ICandyDao _candyDao;

    public CandyService(ICandyDao candyDao)
    {
        _candyDao = candyDao;
    }
    
    public List<Candy> GetCandies()
    {
        return _candyDao.GetCandies();
    }

    public Candy GetCandy(long candyId)
    {
        return _candyDao.GetCandy(candyId);
    }

    public Candy Save(Candy candy)
    {
        return _candyDao.Save(candy);
    }

    public Candy Update(long candyId, Candy candy)
    {
        return _candyDao.Update(candyId, candy);
    }
}