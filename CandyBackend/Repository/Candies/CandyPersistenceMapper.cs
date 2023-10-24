using AutoMapper;
using CandyBackend.Core;

namespace CandyBackend.Repository.Candies;

public class CandyPersistenceMapper : Profile
{
    public CandyPersistenceMapper()
    {
        CreateMap<CandyEntity, Candy>().ReverseMap();
    }

}