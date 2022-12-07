public class AocDirectory
{
    private readonly string name;
    private ICollection<AoCFile> files;
    public ICollection<AocDirectory> Directories { get; }
    public AocDirectory Parent { get; }
    public AocDirectory(string name, AocDirectory parent)
    {
        this.name = name;
        this.files = new List<AoCFile>();
        this.Directories = new List<AocDirectory>();
        this.Parent = parent;
    }

    internal void AddFile(AoCFile file)
    {
        files.Add(file);
    }
    internal void AddDirectory(string name)
    {
        Directories.Add(new AocDirectory(name, this));
    }

    internal AocDirectory GetDirectory(string dirName)
    {
        return Directories.First(d => d.name.Equals(dirName));
    }

    internal int TotalSize
    {
        get
        {
            var fileSizes = files.Sum(f => f.size);
            var dirSizes = Directories.Sum(d => d.TotalSize);
            return fileSizes + dirSizes;
        }
    }

    internal List<AocDirectory> AllSubDirectories
    {
        get
        {
            var allSubDirectories = new List<AocDirectory>();
            foreach (var dir in Directories)
            {
                allSubDirectories.AddRange(dir.AllSubDirectories);
            }
            allSubDirectories.Add(this);
            return allSubDirectories;
        }
    }
}

public record AoCFile(string name, int size);

