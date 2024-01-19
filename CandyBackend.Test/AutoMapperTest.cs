using Xunit;

namespace CandyBackend.Test;

public class AutoMapperTest
{
    [Fact]
    public void MustBeValid()
    {
        AutoMapperConfiguration.Validate();
    }
}