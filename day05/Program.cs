var somethingSomething = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
//    .Select(l => new SomethingSomething(l))
    .ToList();