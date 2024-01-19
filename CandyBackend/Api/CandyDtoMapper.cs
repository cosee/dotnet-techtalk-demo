using AutoMapper;
using CandyBackend.Api.Dto;
using CandyBackend.Core;

namespace CandyBackend.Api;

public class CandyDtoMapper : Profile
{
    public CandyDtoMapper()
    {
        CreateMap<Candy, CandyOut>();
        CreateMap<CandyIn, Candy>()
            .ForMember(c => c.Id, opt => opt.Ignore());
    }
}