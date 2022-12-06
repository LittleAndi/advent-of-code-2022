using Shouldly;

namespace day06tests;

public class UnitTest1
{
    [Theory]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 4, 7)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 4, 5)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 4, 6)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 4, 10)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 4, 11)]
    public void ShouldGetMarkerEnd(string signal, int markerSize, int expectedMarkerEnd)
    {
        var detector = new StartOfPacketMarkerDetector(signal);
        detector.GetMarkerEnd(markerSize).ShouldBe(expectedMarkerEnd);
    }
}