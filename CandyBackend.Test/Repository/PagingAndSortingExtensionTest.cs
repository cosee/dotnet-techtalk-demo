using CandyBackend.Core;
using CandyBackend.Repository;
using FluentAssertions;
using Xunit;

namespace CandyBackend.Test.Repository;

public class PagingAndSortingExtensionTest
{
    [Fact]
    public void AssertAllSortingOptionsAreImplemented()
    {
        foreach (var candySortBy in Enum.GetValues<CandySortBy>())
        {
            var act = () =>
            {
                var comparatorForCandySortBy = PagingAndSortingExtension.GetComparatorForCandySortBy(candySortBy);
                comparatorForCandySortBy.Should().NotBeNull();
            };
            act.Should().NotThrow();
        }
    }
}