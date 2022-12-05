using System.Text.RegularExpressions;

var regex = new Regex(@"move (\d+) from (\d+) to (\d+)");

var stacks1 = ReadStacks("input.txt");
var movements1 = ReadMovements("input.txt");
Part1(stacks1, movements1);

var stacks2 = ReadStacks("input.txt");
var movements2 = ReadMovements("input.txt");
Part2(stacks2, movements2);

void Part1(Stack<char>[] stacks, List<string> movements)
{
    foreach (var moveInstruction in movements)
    {
        var match = regex.Match(moveInstruction);
        var quantity = int.Parse(match.Groups[1].Value);
        var from = int.Parse(match.Groups[2].Value) - 1;
        var to = int.Parse(match.Groups[3].Value) - 1;

        for (int i = 0; i < quantity; i++)
        {
            stacks[to].Push(stacks[from].Pop());
        }
    }

    System.Console.WriteLine(stacks.Select(s => s.Peek()).ToArray());
}

void Part2(Stack<char>[] stacks, List<string> movements)
{
    foreach (var moveInstruction in movements)
    {
        var match = regex.Match(moveInstruction);
        var quantity = int.Parse(match.Groups[1].Value);
        var from = int.Parse(match.Groups[2].Value) - 1;
        var to = int.Parse(match.Groups[3].Value) - 1;

        // Create temp stack to be able to reverse it
        var tempStack = new Stack<char>();
        for (int i = 0; i < quantity; i++)
        {
            tempStack.Push(stacks[from].Pop());
        }

        while (tempStack.Count > 0)
        {
            stacks[to].Push(tempStack.Pop());
        }
    }

    System.Console.WriteLine(stacks.Select(s => s.Peek()).ToArray());
}

Stack<char>[] ReadStacks(string file)
{
    var stacksInput = File.ReadAllLines(file)
        .Take(8)
        .ToList();

    Stack<char>[] reversedStacks = new Stack<char>[9]
    {
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
    };

    foreach (var item in stacksInput)
    {
        for (int i = 0; i < 9; i++)
        {
            var c = item[i * 4 + 1];
            if (c != ' ')
            {
                reversedStacks[i].Push(c);
            }
        }
    }

    Stack<char>[] stacks = new Stack<char>[9]
    {
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
    };

    for (int i = 0; i < 9; i++)
    {
        while (reversedStacks[i].Count != 0)
        {
            stacks[i].Push(reversedStacks[i].Pop());
        }
    }

    return stacks;
}

List<string> ReadMovements(string file)
{
    var movements = File.ReadAllLines(file)
        .Skip(10)
        .ToList();

    return movements;
}