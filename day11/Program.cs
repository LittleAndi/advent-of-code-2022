using System.Text.RegularExpressions;

var notes = File.ReadAllLines("input.txt")
    .ToList();

Part1(notes);
Part2(notes);

void Part1(List<string> notes)
{
    var game = new KeepAwayGame();

    // 8 monkeys in input
    var monkeyStart = 0;
    for (int monkeyIndex = 0; monkeyIndex < 8; monkeyIndex++)
    {
        monkeyStart = monkeyIndex * 7;
        var startItems = notes[monkeyStart + 1][18..].Split(',').Select(i => int.Parse(i)).ToArray();
        var operation = notes[monkeyStart + 2][19..];
        var divisible = int.Parse(notes[monkeyStart + 3][21..]);
        var monkeyIfTrue = int.Parse(notes[monkeyStart + 4][29..]);
        var monkeyIfFalse = int.Parse(notes[monkeyStart + 5][30..]);
        var monkey = new Monkey(startItems, operation, new Test(divisible, monkeyIfTrue, monkeyIfFalse));
        game.AddMonkey(monkey);
    }

    // run 20 rounds
    for (int round = 0; round < 20; round++)
    {
        game.DoRound(true);
    }

    System.Console.WriteLine($"Part 1: Monkey Business after 20 rounds {game.MonkeyBusiness}");
}

void Part2(List<string> notes)
{
    var game = new KeepAwayGame();

    // 8 monkeys in input
    var monkeyStart = 0;
    for (int monkeyIndex = 0; monkeyIndex < 8; monkeyIndex++)
    {
        monkeyStart = monkeyIndex * 7;
        var startItems = notes[monkeyStart + 1][18..].Split(',').Select(i => int.Parse(i)).ToArray();
        var operation = notes[monkeyStart + 2][19..];
        var divisible = int.Parse(notes[monkeyStart + 3][21..]);
        var monkeyIfTrue = int.Parse(notes[monkeyStart + 4][29..]);
        var monkeyIfFalse = int.Parse(notes[monkeyStart + 5][30..]);
        var monkey = new Monkey(startItems, operation, new Test(divisible, monkeyIfTrue, monkeyIfFalse));
        game.AddMonkey(monkey);
    }

    // run 10k rounds
    for (int round = 0; round < 10000; round++)
    {
        game.DoRound(false);
    }

    System.Console.WriteLine($"Part 2: Monkey Business after 10k rounds {game.MonkeyBusiness}");
}

public class Monkey
{
    private Queue<long> itemWorryLevels = new Queue<long>();
    public readonly Test Test;
    private readonly Regex operationRegex = new Regex(@"old ([*\+]) (old|\d+)");
    private readonly string operation;
    private readonly Match operationMatch;
    private readonly char op;

    public Monkey(int[] startItems, string operation, Test test)
    {
        startItems.ToList().ForEach(i => itemWorryLevels.Enqueue(i));
        this.operation = operation;
        this.Test = test;

        operationMatch = operationRegex.Match(operation);
        op = operationMatch.Groups[1].Value[0];
    }

    public long ItemsInspected { get; private set; }

    internal void Turn(List<Monkey> monkeysToThrowTo, bool divideByThree, long leastCommonMultiple)
    {
        while (itemWorryLevels.Count > 0)
        {
            var itemWorryLevel = itemWorryLevels.Dequeue();
            ItemsInspected++;
            var rightOperand = operationMatch.Groups[2].Value.Equals("old") ? itemWorryLevel : long.Parse(operationMatch.Groups[2].Value);

            long newWorryLevel = 0;
            switch (op)
            {
                case '+':
                    newWorryLevel = (itemWorryLevel + rightOperand);
                    break;
                case '*':
                    newWorryLevel = (itemWorryLevel * rightOperand);
                    break;
            }

            // Put this in here for part 2...
            if (newWorryLevel < 0)
            {
                System.Console.WriteLine("Overflow...");
            }

            if (divideByThree) newWorryLevel = newWorryLevel / 3;
            else newWorryLevel %= leastCommonMultiple;

            if (newWorryLevel % Test.Divisible == 0) monkeysToThrowTo[Test.MonkeyIfTrue].Receive(newWorryLevel);
            else monkeysToThrowTo[Test.MonkeyIfFalse].Receive(newWorryLevel);
        }
    }

    private void Receive(long newWorryLevel)
    {
        itemWorryLevels.Enqueue(newWorryLevel);
    }
}

public record Test(long Divisible, int MonkeyIfTrue, int MonkeyIfFalse);

public class KeepAwayGame
{
    public List<Monkey> Monkeys = new List<Monkey>();
    private long leastCommonMultiple;

    public long MonkeyBusiness => Monkeys.Select(m => m.ItemsInspected).OrderByDescending(i => i).Take(2).Aggregate((a, b) => a * b);

    public void AddMonkey(Monkey monkey)
    {
        Monkeys.Add(monkey);

        // Update Least Common Multiple for each added monkey
        leastCommonMultiple = LCM(Monkeys.Select(m => m.Test.Divisible).ToArray());
    }

    public void DoRound(bool divideByThree)
    {
        foreach (var monkey in Monkeys)
        {
            monkey.Turn(Monkeys, divideByThree, leastCommonMultiple);
        }
    }

    // From https://stackoverflow.com/questions/147515/least-common-multiple-for-3-or-more-numbers/29717490#29717490
    static long LCM(long[] numbers)
    {
        return numbers.Aggregate(lcm);
    }
    static long lcm(long a, long b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }
    static long GCD(long a, long b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }
}