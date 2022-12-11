using Shouldly;

namespace day11tests;

public class UnitTest1
{
    [Fact]
    public void ShouldCalculateMonkeyBusinessAfter20RoundsWithDivideByThree()
    {
        var game = new KeepAwayGame();
        game.AddMonkey(new Monkey(new int[] { 79, 98 }, "old * 19", new Test(23, 2, 3)));
        game.AddMonkey(new Monkey(new int[] { 54, 65, 75, 74 }, "old + 6", new Test(19, 2, 0)));
        game.AddMonkey(new Monkey(new int[] { 79, 60, 97 }, "old * old", new Test(13, 1, 3)));
        game.AddMonkey(new Monkey(new int[] { 74 }, "old + 3", new Test(17, 0, 1)));
        for (int i = 0; i < 20; i++)
        {
            game.DoRound(true);
        }
        game.MonkeyBusiness.ShouldBe(10605);
    }

    [Fact]
    public void ShouldCalculateMonkeyBusinessAfter10kRoundsWithoutDivideByThree()
    {
        var game = new KeepAwayGame();
        game.AddMonkey(new Monkey(new int[] { 79, 98 }, "old * 19", new Test(23, 2, 3)));
        game.AddMonkey(new Monkey(new int[] { 54, 65, 75, 74 }, "old + 6", new Test(19, 2, 0)));
        game.AddMonkey(new Monkey(new int[] { 79, 60, 97 }, "old * old", new Test(13, 1, 3)));
        game.AddMonkey(new Monkey(new int[] { 74 }, "old + 3", new Test(17, 0, 1)));
        for (int i = 0; i < 10000; i++)
        {
            game.DoRound(false);
        }
        game.MonkeyBusiness.ShouldBe(2713310158);
    }

    [Fact]
    public void ShouldReturnItemsInspected()
    {
        var game = new KeepAwayGame();
        game.AddMonkey(new Monkey(new int[] { 79, 98 }, "old * 19", new Test(23, 2, 3)));
        game.AddMonkey(new Monkey(new int[] { 54, 65, 75, 74 }, "old + 6", new Test(19, 2, 0)));
        game.AddMonkey(new Monkey(new int[] { 79, 60, 97 }, "old * old", new Test(13, 1, 3)));
        game.AddMonkey(new Monkey(new int[] { 74 }, "old + 3", new Test(17, 0, 1)));

        // After round 1
        game.DoRound(false);
        game.Monkeys[0].ItemsInspected.ShouldBe(2);
        game.Monkeys[1].ItemsInspected.ShouldBe(4);
        game.Monkeys[2].ItemsInspected.ShouldBe(3);
        game.Monkeys[3].ItemsInspected.ShouldBe(6);

        // After round 20
        for (int i = 0; i < 20 - 1; i++)
        {
            game.DoRound(false);
        }
        game.Monkeys[0].ItemsInspected.ShouldBe(99);
        game.Monkeys[1].ItemsInspected.ShouldBe(97);
        game.Monkeys[2].ItemsInspected.ShouldBe(8);
        game.Monkeys[3].ItemsInspected.ShouldBe(103);

        // After round 1000
        for (int i = 0; i < 1000 - 20; i++)
        {
            game.DoRound(false);
        }
        game.Monkeys[0].ItemsInspected.ShouldBe(5204);
        game.Monkeys[1].ItemsInspected.ShouldBe(4792);
        game.Monkeys[2].ItemsInspected.ShouldBe(199);
        game.Monkeys[3].ItemsInspected.ShouldBe(5192);
    }
}