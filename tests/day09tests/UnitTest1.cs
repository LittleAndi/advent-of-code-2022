using Shouldly;

namespace day09tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var rope = new Rope();
        rope.MoveHead("R 4");
        rope.Tail.ShouldBe(new Point { X = 3, Y = 0 });
        rope.MoveHead("U 4");
        rope.Tail.ShouldBe(new Point { X = 4, Y = -3 });
        rope.MoveHead("L 3");
        rope.MoveHead("D 1");
        rope.MoveHead("R 4");
        rope.MoveHead("D 1");
        rope.MoveHead("L 5");
        rope.MoveHead("R 2");
        rope.Tail.ShouldBe(new Point { X = 1, Y = -2 });
    }

    [Fact]
    public void Test2()
    {
        var tailPositions = new List<Point>();
        var rope = new Rope();
        tailPositions.AddRange(rope.MoveHead("R 4"));
        tailPositions.AddRange(rope.MoveHead("U 4"));
        tailPositions.AddRange(rope.MoveHead("L 3"));
        tailPositions.AddRange(rope.MoveHead("D 1"));
        tailPositions.AddRange(rope.MoveHead("R 4"));
        tailPositions.AddRange(rope.MoveHead("D 1"));
        tailPositions.AddRange(rope.MoveHead("L 5"));
        tailPositions.AddRange(rope.MoveHead("R 2"));
        tailPositions.ToHashSet().Count.ShouldBe(13);
    }
}