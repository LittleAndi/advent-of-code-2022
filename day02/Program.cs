var lines = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => new RockPaperScissorsRound(l[0], l[2]))
    .ToList();

System.Console.WriteLine(lines.Sum(l => l.MyScore));