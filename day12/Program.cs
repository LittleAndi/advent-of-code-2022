using System.Diagnostics.CodeAnalysis;

var heightMapInfo = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => l.ToCharArray())
    .ToArray();

var heightMap = new HeightMap(heightMapInfo);

//System.Console.WriteLine(heightMap.PrinterFriendlyScreenLines[0]);

System.Console.WriteLine(heightMap.ToString());

public class HeightMap
{
    char[,] heightMap;
    int xSize;
    int ySize;
    Point start;
    public HeightMap(char[][] mapInput)
    {
        xSize = mapInput[0].Length;
        ySize = mapInput.Length;

        heightMap = new char[xSize, ySize];

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                heightMap[x, y] = mapInput[y][x];
                if (heightMap[x, y].Equals('S')) start = new Point(x, y);
            }
        }
    }

    public int Climb()
    {
        var path = new List<Point>();
        Find(start, path);
        return path.Count;
    }

    public int Find(Point p, List<Point> path)
    {
        // Add yourself
        path.Add(p);

        // Return if end
        if (heightMap[p.X, p.Y].Equals('E')) return path.Count;

        if (heightMap[p.X, p.Y].Equals('z') && heightMap[p.Up.X, p.Up.Y].Equals('E')) return 1;
        if (heightMap[p.X, p.Y].Equals('z') && heightMap[p.Left.X, p.Left.Y].Equals('E')) return 1;
        if (heightMap[p.X, p.Y].Equals('z') && heightMap[p.Down.X, p.Down.Y].Equals('E')) return 1;
        if (heightMap[p.X, p.Y].Equals('z') && heightMap[p.Right.X, p.Right.Y].Equals('E')) return 1;

        // Look around (we should have not been there before)
        // Find next if higher or same
        var directionLengths = new List<int>();
        if (!path.Contains(p.Up) && !IsOutOfBounds(p.Up) && heightMap[p.Up.X, p.Up.Y] >= heightMap[p.X, p.Y]) directionLengths.Add(Find(p.Up, path));
        if (!path.Contains(p.Left) && !IsOutOfBounds(p.Left) && heightMap[p.Left.X, p.Left.Y] >= heightMap[p.X, p.Y]) directionLengths.Add(Find(p.Left, path));
        if (!path.Contains(p.Down) && !IsOutOfBounds(p.Down) && heightMap[p.Down.X, p.Down.Y] >= heightMap[p.X, p.Y]) directionLengths.Add(Find(p.Down, path));
        if (!path.Contains(p.Right) && !IsOutOfBounds(p.Right) && heightMap[p.Right.X, p.Right.Y] >= heightMap[p.X, p.Y]) directionLengths.Add(Find(p.Right, path));

        if (directionLengths.Count == 0)
        {
            System.Console.WriteLine("ERROR");
        }

        return directionLengths.OrderBy(l => l).First();
    }

    private bool IsOutOfBounds(Point p)
    {
        if (p.X < 0) return true;
        if (p.X >= heightMap.GetLength(0)) return true;
        if (p.Y < 0) return true;
        if (p.Y >= heightMap.GetLength(1)) return true;
        return false;
    }

    public List<string> PrinterFriendlyScreenLines
    {
        get
        {
            var lines = new List<string>();
            for (int y = 0; y < heightMap.GetLength(1); y++)
            {
                lines.Add(new string(Enumerable.Range(0, heightMap.GetLength(0)).Select(x => heightMap[x, y]).ToArray()));
            }
            return lines;
        }
    }

    public override string ToString()
    {
        return string.Join("\n", PrinterFriendlyScreenLines);
    }

}

public struct Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X { get; set; }
    public int Y { get; set; }
    public Point Up => new Point { X = X, Y = Y - 1 };
    public Point Left => new Point { X = X - 1, Y = Y };
    public Point Down => new Point { X = X, Y = Y + 1 };
    public Point Right => new Point { X = X + 1, Y = Y };

    public override bool Equals([NotNullWhen(true)] object obj) => (((Point)obj).X == this.X && ((Point)obj).Y == this.Y);

    public override int GetHashCode()
    {
        return (X, Y).GetHashCode();
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}
