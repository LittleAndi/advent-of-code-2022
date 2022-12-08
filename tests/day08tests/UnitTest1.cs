using Shouldly;

namespace day08tests;

public class UnitTest1
{
    TreeMap treeMap;

    public UnitTest1()
    {
        var treeMapInfo = File.ReadAllLines("input_test.txt")
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => l.ToCharArray())
            .ToArray();
        treeMap = new TreeMap(treeMapInfo);
    }
    [Fact]
    public void Test1()
    {
        // treeMap.TopVisible(1, 1).ShouldBeFalse();
        // treeMap.BottomVisible(1, 1).ShouldBeFalse();
        // treeMap.LeftVisible(1, 1).ShouldBeFalse();
        // treeMap.RightVisible(1, 1).ShouldBeFalse();

        // Andra raden
        treeMap.TopVisible(1, 1).ShouldBeTrue();
        treeMap.BottomVisible(1, 1).ShouldBeFalse();
        treeMap.LeftVisible(1, 1).ShouldBeTrue();
        treeMap.RightVisible(1, 1).ShouldBeFalse();

        treeMap.TopVisible(2, 1).ShouldBeTrue();
        treeMap.BottomVisible(2, 1).ShouldBeFalse();
        treeMap.LeftVisible(2, 1).ShouldBeFalse();
        treeMap.RightVisible(2, 1).ShouldBeTrue();

        treeMap.TopVisible(3, 1).ShouldBeFalse();
        treeMap.BottomVisible(3, 1).ShouldBeFalse();
        treeMap.LeftVisible(3, 1).ShouldBeFalse();
        treeMap.RightVisible(3, 1).ShouldBeFalse();

        // Tredje raden
        treeMap.TopVisible(1, 2).ShouldBeFalse();
        treeMap.BottomVisible(1, 2).ShouldBeFalse();
        treeMap.LeftVisible(1, 2).ShouldBeFalse();
        treeMap.RightVisible(1, 2).ShouldBeTrue();

        treeMap.VisibleTrees.ShouldBe(21);
    }

    [Fact]
    public void ShouldScenicScore()
    {
        treeMap.TopVisibleCount(2, 1).ShouldBe(1);
        treeMap.LeftVisibleCount(2, 1).ShouldBe(1);
        treeMap.BottomVisibleCount(2, 1).ShouldBe(2);
        treeMap.RightVisibleCount(2, 1).ShouldBe(2);
        treeMap.ScenicScore(2, 1).ShouldBe(4);

        treeMap.TopVisibleCount(2, 3).ShouldBe(2);
        treeMap.LeftVisibleCount(2, 3).ShouldBe(2);
        treeMap.BottomVisibleCount(2, 3).ShouldBe(1);
        treeMap.RightVisibleCount(2, 3).ShouldBe(2);
        treeMap.ScenicScore(2, 3).ShouldBe(8);

        treeMap.BestScenicScore.ShouldBe(8);
    }
}