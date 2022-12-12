using Shouldly;

namespace day12tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var heightMapInfo = File.ReadAllLines("input_test.txt")
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => l.ToCharArray())
            .ToArray();
        var heightMap = new HeightMap(heightMapInfo);
        var steps = heightMap.Climb();
        steps.ShouldBe(31);
    }
}