using NUnit.Framework;

namespace CandyBackend.Test;

[TestFixture]
public class AutomapperTest
{
    [Test]
    public void MustBeValid()
    {
        AutomapperConfiguration.Validate();
    }
}