using Xunit;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

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
    public void TestPart1(char opponent, char me, int expectedScore)
    {
        var round = new RockPaperScissorsRound(opponent, me);
        round.MyScore.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData('A', 'Y', 1 + 3)]
    [InlineData('B', 'X', 1 + 0)]
    [InlineData('C', 'Z', 1 + 6)]
    public void TestPart2(char opponent, char ending, int expectedScore)
    {
        var round = new RockPaperScissorsRoundWithEnding(opponent, ending);
        round.MyScore.ShouldBe(expectedScore);
    }
    [Fact]
    public void TestPart2Sum()
    {
        var rounds = new List<RockPaperScissorsRoundWithEnding>();
        rounds.Add(new RockPaperScissorsRoundWithEnding('A', 'Y'));
        rounds.Add(new RockPaperScissorsRoundWithEnding('B', 'X'));
        rounds.Add(new RockPaperScissorsRoundWithEnding('C', 'Z'));
        rounds.Sum(r => r.MyScore).ShouldBe(12);
    }
}