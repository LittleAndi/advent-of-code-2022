var part1rounds = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => new RockPaperScissorsRound(l[0], l[2]))
    .ToList();

System.Console.WriteLine(part1rounds.Sum(l => l.MyScore));

var part2rounds = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => new RockPaperScissorsRoundWithEnding(l[0], l[2]))
    .ToList();

System.Console.WriteLine(part2rounds.Sum(l => l.MyScore));

