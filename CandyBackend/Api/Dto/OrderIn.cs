using System.ComponentModel.DataAnnotations;

namespace CandyBackend.Api.Dto;

public class OrderIn
{
    [Required]
    [MinLength(3)]
    public required string FirstName { get; init; }
    
    [Required]
    [MinLength(3)]
    public required string LastName { get; init; }
    
    [Required]
    [MinLength(1)]
    public required List<long> CandyIds { get; init; }
}