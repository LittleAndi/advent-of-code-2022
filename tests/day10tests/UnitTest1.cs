using Shouldly;

namespace day10tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var cpu = new Cpu(new List<string> { "noop", "addx 3", "addx -5" });
        var inspectionSignalStrengths = cpu.Run();
        inspectionSignalStrengths.ShouldBeEmpty();
    }

    [Fact]
    public void ShouldGetSumOfInspectedSignalStrengths()
    {
        var instructions = File.ReadAllLines("input_test.txt")
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();
        var cpu = new Cpu(instructions);
        var inspectionSignalStrengths = cpu.Run();
        inspectionSignalStrengths.Sum().ShouldBe(13140);
    }

    [Fact]
    public void TestCrt()
    {
        var instructions = File.ReadAllLines("input_test.txt")
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();
        var cpu = new Cpu(instructions);
        cpu.Run();

        Enumerable.Range(0, cpu.Crt.Screen.GetLength(1))
            .Select(x => cpu.Crt.Screen[0, x])
            .ToArray()
            .ShouldBe("##..##..##..##..##..##..##..##..##..##..");

        Enumerable.Range(0, cpu.Crt.Screen.GetLength(1))
            .Select(x => cpu.Crt.Screen[1, x])
            .ToArray()
            .ShouldBe("###...###...###...###...###...###...###.");

        Enumerable.Range(0, cpu.Crt.Screen.GetLength(1))
            .Select(x => cpu.Crt.Screen[2, x])
            .ToArray()
            .ShouldBe("####....####....####....####....####....");

        Enumerable.Range(0, cpu.Crt.Screen.GetLength(1))
            .Select(x => cpu.Crt.Screen[3, x])
            .ToArray()
            .ShouldBe("#####.....#####.....#####.....#####.....");

        Enumerable.Range(0, cpu.Crt.Screen.GetLength(1))
            .Select(x => cpu.Crt.Screen[4, x])
            .ToArray()
            .ShouldBe("######......######......######......####");

        Enumerable.Range(0, cpu.Crt.Screen.GetLength(1))
            .Select(x => cpu.Crt.Screen[5, x])
            .ToArray()
            .ShouldBe("#######.......#######.......#######.....");
    }
}