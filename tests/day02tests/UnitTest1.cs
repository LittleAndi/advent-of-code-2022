using Xunit;
using Shouldly;
namespace day02tests;

public class UnitTest1
{
    [Theory]
    [InlineData('A', 'Y', 2 + 6)]
    [InlineData('B', 'X', 1 + 0)]
    [InlineData('C', 'Z', 3 + 3)]

    [InlineData('A', 'X', 1 + 3)]
    [InlineData('B', 'Y', 2 + 3)]
    [InlineData('C', 'Z', 3 + 3)]

    [InlineData('A', 'Y', 2 + 6)]
    [InlineData('B', 'Z', 3 + 6)]
    [InlineData('C', 'X', 1 + 6)]

    [InlineData('A', 'Z', 3 + 0)]
    [InlineData('B', 'X', 1 + 0)]
    [InlineData('C', 'Y', 2 + 0)]
    public void Test1(char opponent, char me, int score)
    {
        var round = new RockPaperScissorsRound(opponent, me);
        round.MyScore.ShouldBe(score);
    }
}