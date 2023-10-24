namespace CandyBackend.Core;

public class Candy
{
    public long Id { get; init; }

    public string Name { get; set; } = null!;

    public long Price { get; set; }
}