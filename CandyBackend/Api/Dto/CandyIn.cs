using System.ComponentModel.DataAnnotations;

namespace CandyBackend.Api.Dto;

public class CandyIn
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Name { get; init; }
    
    [Required]
    [Range(1, 1000)]
    public required long Price { get; init; }
}