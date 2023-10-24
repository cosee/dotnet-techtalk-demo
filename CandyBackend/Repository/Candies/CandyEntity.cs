using System.ComponentModel.DataAnnotations.Schema;

namespace CandyBackend.Repository.Candies;

[Table("candy")]
public class CandyEntity
{
    public long Id { get; init; }

    public string Name { get; set; } = null!;

    public long Price { get; set; }
}