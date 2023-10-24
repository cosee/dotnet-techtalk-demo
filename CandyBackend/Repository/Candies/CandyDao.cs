using AutoMapper;
using CandyBackend.Core;

namespace CandyBackend.Repository.Candies;

public class CandyDao : ICandyDao
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public CandyDao(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public Candy Save(Candy candy)
    {
        var candyEntity = _mapper.Map<CandyEntity>(candy);
        _context.Candy.Add(candyEntity);
        _context.SaveChanges();
        return _mapper.Map<Candy>(candyEntity);
    }

    public Candy Update(long candyId, Candy candy)
    {
        using var transaction = _context.Database.BeginTransaction();

        var candyEntity = _context.Candy.First(c => c.Id == candyId);
        _mapper.Map(candy, candyEntity);

        _context.SaveChanges();

        transaction.Commit();

        return _mapper.Map<Candy>(candyEntity);
    }

    public Candy GetCandy(long candyId)
    {
        return _mapper.Map<Candy>(_context.Candy.First(c => c.Id == candyId));
    }

    public List<Candy> GetCandies()
    {
        return _mapper.Map<List<Candy>>(_context.Candy);
    }

    public List<Candy> FindCandiesById(ICollection<long> candyIds)
    {
        return _mapper.Map<List<Candy>>(_context.Candy.Where(c => candyIds.Contains(c.Id)));
    }
}