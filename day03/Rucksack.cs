public class Rucksack
{
    char[] compartmentOne;
    char[] compartmentTwo;
    public char[] Contents;
    public Rucksack(string contents)
    {
        Contents = contents.ToArray();
        compartmentOne = contents.Take(contents.Length / 2).ToArray();
        compartmentTwo = contents.TakeLast(contents.Length / 2).ToArray();
    }

    public int DuplicateItemTypePriority
    {
        get
        {
            var intersectingItemTypes = compartmentOne.Intersect(compartmentTwo);
            var firstIntersectingItemType = intersectingItemTypes.First();
            int priority = char.IsLower(firstIntersectingItemType) ? firstIntersectingItemType - 96 : firstIntersectingItemType - 64 + 26;
            return priority;
        }
    }
}

public class RucksackGroup
{
    private readonly Rucksack[] rucksacks;

    public RucksackGroup(Rucksack[] rucksacks)
    {
        this.rucksacks = rucksacks;
    }

    public int CommonItemTypePriority
    {
        get
        {
            var commonItemTypes = rucksacks[0].Contents.Intersect(rucksacks[1].Contents.Intersect(rucksacks[2].Contents));
            var firstCommonItemType = commonItemTypes.First();
            int priority = char.IsLower(firstCommonItemType) ? firstCommonItemType - 96 : firstCommonItemType - 64 + 26;
            return priority;
        }
    }
}
