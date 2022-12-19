using Shouldly;

namespace day13tests;

public class UnitTest1
{
    [Theory]
    // [InlineData("[1,1,3,1,1]", "[1,1,5,1,1]", false)]
    [InlineData("[1,[2,[3,[4,[5,6,7]]]],8,9]", "[1,[2,[3,[4,[5,6,0]]]],8,9]", false)]
    // [InlineData("[[1],[2,3,4]]", "[[1],4]", false)]
    public void Test1(string left, string right, bool expectedInTheRightOrder)
    {
        var signalDecoder = new SignalDecoder(left, right);
        signalDecoder.InTheRightOrder.ShouldBe(expectedInTheRightOrder);
    }
}