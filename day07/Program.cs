using System.Text.RegularExpressions;

var commandsAndContent = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .ToList();

var root = new AocDirectory("/", null);
var currentDir = root;

PopulateTree(commandsAndContent);

System.Console.WriteLine($"Total size: {root.TotalSize}");

// Part 1
var dirsWithMaxTotalSize100k = DirsWithMaxTotalSize100k(new AocDirectory[] { root }.ToList());
System.Console.WriteLine($"Part 1: {dirsWithMaxTotalSize100k.Sum(d => d.TotalSize)}");

// Part 2
var allDirectories = root.AllSubDirectories;
var freeSpace = 70000000 - root.TotalSize;
System.Console.WriteLine($"Free space: {freeSpace}");
System.Console.WriteLine($"Part 2: {allDirectories.Where(d => freeSpace + d.TotalSize >= 30000000).OrderBy(d => d.TotalSize).First().TotalSize}");

void PopulateTree(List<string> commandsAndContent)
{
    Regex regexDir = new Regex(@"dir ([a-z]+)");
    Regex regexFile = new Regex(@"(\d+) ([a-z.]+)");

    for (int i = 1; i < commandsAndContent.Count; i++)
    {
        if (commandsAndContent[i][0].Equals('$'))
        {
            // Command
            var command = commandsAndContent[i][2..4];
            switch (command)
            {
                case "cd":
                    var dirName = commandsAndContent[i][5..];
                    if (dirName.Equals(".."))
                    {
                        currentDir = currentDir.Parent;
                    }
                    else
                    {
                        currentDir = currentDir.GetDirectory(dirName);
                    }
                    break;
                case "ls":
                    i++;
                    while (i < commandsAndContent.Count && !commandsAndContent[i][0].Equals('$'))
                    {
                        var listItem = commandsAndContent[i];

                        if (regexDir.IsMatch(listItem))
                        {
                            var dirMatch = regexDir.Match(listItem);
                            currentDir.AddDirectory(dirMatch.Groups[1].Value);
                        }

                        if (regexFile.IsMatch(listItem))
                        {
                            var fileMatch = regexFile.Match(listItem);
                            var name = fileMatch.Groups[2].Value;
                            var size = fileMatch.Groups[1].Value;
                            currentDir.AddFile(new AoCFile(name, int.Parse(size)));
                        }
                        i++;
                    }
                    i--;
                    break;
            }
        }
    }
}

List<AocDirectory> DirsWithMaxTotalSize100k(List<AocDirectory> dirsToTest)
{
    List<AocDirectory> dirsWithMax100k = new List<AocDirectory>();

    foreach (var dir in dirsToTest)
    {
        if (dir.TotalSize <= 100000) dirsWithMax100k.Add(dir);
        dirsWithMax100k.AddRange(DirsWithMaxTotalSize100k(dir.Directories.ToList()));
    }

    return dirsWithMax100k;
}
