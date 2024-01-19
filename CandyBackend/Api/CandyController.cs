using AutoMapper;
using CandyBackend.Api.Dto;
using CandyBackend.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(Summary = "Returns all candies")]
    public IEnumerable<CandyOut> Get(
        [FromQuery] int limit = 20,
        [FromQuery] int offset = 0,
        [FromQuery] CandySortBy sortBy = CandySortBy.Name,
        [FromQuery] SortDir sortDir = SortDir.Asc
    )
    {
        return _mapper.Map<IEnumerable<CandyOut>>(_candyService.GetCandies(limit, offset, sortBy, sortDir));
    }

    [HttpGet("{candyId:long}")]
    [SwaggerOperation(Summary = "Returns a candy by its id")]
    public CandyOut Get(long candyId)
    {
        return _mapper.Map<CandyOut>(_candyService.GetCandy(candyId));
    }

    [Authorize(Roles = "CandyManager")]
    [HttpPost]
    [SwaggerOperation(Summary = "Creates a new candy")]
    public CandyOut Save(CandyIn candyIn)
    {
        return _mapper.Map<CandyOut>(_candyService.Save(_mapper.Map<Candy>(candyIn)));
    }

    [Authorize(Roles = "CandyManager")]
    [HttpPut("{candyId:long}")]
    [SwaggerOperation(Summary = "Updates a candy by its id")]
    public CandyOut Update(long candyId, CandyIn candyIn)
    {
        return _mapper.Map<CandyOut>(_candyService.Update(candyId, _mapper.Map<Candy>(candyIn)));
    }
}