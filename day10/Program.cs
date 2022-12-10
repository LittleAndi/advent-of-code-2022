var instructions = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .ToList();

var cpu = new Cpu(instructions);
var inspectionSignalStrengths = cpu.Run();
System.Console.WriteLine($"Part 1: Sum of inspected signal strengths are {inspectionSignalStrengths.Sum()}");

System.Console.WriteLine($"Part 2:");
foreach (var line in cpu.Crt.PrinterFriendlyScreenLines)
{
    System.Console.WriteLine(line);
}


public class Cpu
{
    private List<Instruction> instructions;
    private int registerX = 1;
    public Crt Crt;
    public Cpu(List<string> instructions)
    {
        this.instructions = instructions.Select(i => new Instruction(i)).ToList();
        this.Crt = new Crt(40, 6);
    }

    public IEnumerable<int> Run()
    {
        var inspectionAtCycles = new List<int> { 20, 60, 100, 140, 180, 220 };
        var inspectionSignalStrengths = new List<int>();
        var completedAtCycle = 0;
        var instructionsToRun = instructions.Select(i => (inst: i, completedAtCycle: completedAtCycle += i.CyclesToComplete)).ToDictionary(x => x.completedAtCycle, x => x.inst);

        for (int cycle = 1; cycle <= completedAtCycle; cycle++)
        {
            // screen
            Crt.Draw(registerX);

            // computing
            if (inspectionAtCycles.Contains(cycle)) inspectionSignalStrengths.Add(registerX * cycle);
            if (!instructionsToRun.ContainsKey(cycle)) continue;    // Nothing to do during this cycle
            var instruction = instructionsToRun[cycle];
            registerX += instruction.Value;
        }

        return inspectionSignalStrengths;
    }

    record Instruction(string InstructionString)
    {
        public bool IsNoop => InstructionString.Equals("noop");
        public int CyclesToComplete => IsNoop ? 1 : 2;
        public int Value => IsNoop ? 0 : int.Parse(InstructionString[5..]);
    };
}

public class Crt
{
    private char[,] screen;
    private int pixelPosition = 0;
    public Crt(int sizeX, int sizeY)
    {
        this.screen = new char[sizeY, sizeX];
    }

    public void Draw(int spritePosition)
    {
        var pixelPositionX = pixelPosition % 40;
        var pixelPositionY = pixelPosition / 40;

        if (pixelPositionX >= spritePosition - 1 && pixelPositionX <= spritePosition + 1)
        {
            screen[pixelPositionY, pixelPositionX] = '#';
        }
        else
        {
            screen[pixelPositionY, pixelPositionX] = '.';
        }
        pixelPosition++;
    }
    public char[,] Screen => screen;

    public List<string> PrinterFriendlyScreenLines
    {
        get
        {
            var lines = new List<string>();
            for (int row = 0; row < Screen.GetLength(0); row++)
            {
                lines.Add(new string(Enumerable.Range(0, Screen.GetLength(1)).Select(x => Screen[row, x]).ToArray()));
            }
            return lines;
        }
    }
}