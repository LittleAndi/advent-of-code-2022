var lines = File.ReadAllLines("input.txt")
    .ToArray<string>();

var elves = new SortedList<int, int>();

var sum = 0;
var elf = 0;
foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        if (!elves.ContainsKey(sum))
            elves.Add(sum, elf++);
        sum = 0;
    }
    else
    {
        sum += int.Parse(line);
    }
}

// Part 1
System.Console.WriteLine(elves.Last().Key);

// Part 2
System.Console.WriteLine(elves.TakeLast(3).Sum(e => e.Key));
