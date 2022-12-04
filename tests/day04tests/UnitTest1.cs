using Shouldly;
using Xunit;

namespace day04tests;

public class UnitTest1
{
    [Theory]
    [InlineData("2-4,6-8", false)]
    [InlineData("2-3,4-5", false)]
    [InlineData("5-7,7-9", false)]
    [InlineData("2-8,3-7", true)]
    [InlineData("6-6,4-6", true)]
    [InlineData("2-6,4-8", false)]
    public void ShouldFindAssignmentsThatFullyContainsTheOther(string assignments, bool expectedFullyContains)
    {
        var assignment = new SectionAssignmentPair(assignments);
        assignment.OneAssignmentFullyContainsTheOther.ShouldBe(expectedFullyContains);
    }

    [Theory]
    [InlineData("2-4,6-8", false)]
    [InlineData("2-3,4-5", false)]
    [InlineData("5-7,7-9", true)]
    [InlineData("2-8,3-7", true)]
    [InlineData("6-6,4-6", true)]
    [InlineData("2-6,4-8", true)]
    public void ShouldFindAssignmentsThatOverlap(string assignments, bool expectedFullyContains)
    {
        var assignment = new SectionAssignmentPair(assignments);
        assignment.AssignmentsOverlap.ShouldBe(expectedFullyContains);
    }
}