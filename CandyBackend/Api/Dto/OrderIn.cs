using System.ComponentModel.DataAnnotations;

namespace CandyBackend.Api.Dto;

public record OrderIn
{
    [Required]
    [MinLength(3)]
    public required string Name { get; init; }

    [Required]
    [EmailAddress]
    public required string Mail { get; init; }

    [Required]
    [MinLength(1)]
    public required List<long> CandyIds { get; init; }
}