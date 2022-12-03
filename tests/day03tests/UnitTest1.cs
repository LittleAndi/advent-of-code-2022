using Shouldly;
using Xunit;

namespace day03tests;

public class UnitTest1
{
    [Theory]
    [InlineData("vJrwpWtwJgWrhcsFMMfFFhFp", 16)]
    [InlineData("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", 38)]
    [InlineData("PmmdzqPrVvPwwTWBwg", 42)]
    [InlineData("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", 22)]
    [InlineData("ttgJtRGJQctTZtZT", 20)]
    [InlineData("CrZsJsPPZsGzwwsLwLmpwMDw", 19)]
    public void ShouldReturnDuplicateItemTypePriority(string contents, int expectedPriority)
    {
        var rucksack = new Rucksack(contents);
        rucksack.DuplicateItemTypePriority.ShouldBe(expectedPriority);
    }

    [Theory]
    [InlineData("vJrwpWtwJgWrhcsFMMfFFhFp", "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "PmmdzqPrVvPwwTWBwg", 18)]
    [InlineData("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", "ttgJtRGJQctTZtZT", "CrZsJsPPZsGzwwsLwLmpwMDw", 52)]
    public void Test(string content1, string content2, string content3, int expectedPriority)
    {
        var rucksackGroup = new RucksackGroup(new Rucksack[] { new Rucksack(content1), new Rucksack(content2), new Rucksack(content3) });
        rucksackGroup.CommonItemTypePriority.ShouldBe(expectedPriority);
    }

}