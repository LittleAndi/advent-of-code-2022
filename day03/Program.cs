var rucksacks = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => new Rucksack(l))
    .ToList();

System.Console.WriteLine(rucksacks.Sum(r => r.DuplicateItemTypePriority));

var i = 0;
var rucksackGroups = new List<RucksackGroup>();
while (i < rucksacks.Count)
{
    rucksackGroups.Add(new RucksackGroup(new Rucksack[] { rucksacks[i++], rucksacks[i++], rucksacks[i++] }));
}

System.Console.WriteLine(rucksackGroups.Sum(rg => rg.CommonItemTypePriority));
