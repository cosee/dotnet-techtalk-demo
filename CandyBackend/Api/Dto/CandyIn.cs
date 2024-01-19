using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace CandyBackend.Api.Dto;

public record CandyIn
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    [SwaggerParameter(Description = "The name of the candy")]
    public required string Name { get; init; }
    
    [Required]
    [Range(1, 1000)]
    public required long Price { get; init; }
}