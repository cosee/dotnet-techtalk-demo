using AutoMapper;
using CandyBackend.Api.Dto;
using CandyBackend.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CandyBackend.Api;

[ApiController]
[Route("/candies")]
public class CandyController : ControllerBase
{
    private readonly ICandyService _candyService;
    private readonly IMapper _mapper;

    public CandyController(ICandyService candyService, IMapper mapper)
    {
        _candyService = candyService;
        _mapper = mapper;
    }

    [HttpGet]
    public IEnumerable<CandyOut> Get()
    {
        return _mapper.Map<IEnumerable<CandyOut>>(_candyService.GetCandies());
    }

    [HttpGet("{candyId:long}")]
    public CandyOut Get(long candyId)
    {
        return _mapper.Map<CandyOut>(_candyService.GetCandy(candyId));
    }

    [HttpPost]
    public CandyOut Save(CandyIn candyIn)
    {
        return _mapper.Map<CandyOut>(_candyService.Save(_mapper.Map<Candy>(candyIn)));
    }

    [HttpPut("{candyId:long}")]
    public CandyOut Update(long candyId, CandyIn candyIn)
    {
        return _mapper.Map<CandyOut>(_candyService.Update(candyId, _mapper.Map<Candy>(candyIn)));
    }
}