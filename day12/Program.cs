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
        (var success, var path) = Find(start, new List<Point>());
        if (!success)
        {
            System.Console.WriteLine("FAILURE!");
        }
        return path.Count;
    }

    public (bool success, List<Point>) Find(Point p, List<Point> path)
    {
        System.Console.WriteLine($"{p.ToString()}: {heightMap[p.X, p.Y]}");

        // Add yourself
        path.Add(p);

        // Return if end
        if (heightMap[p.X, p.Y].Equals('E')) return (true, new List<Point>());

        // Return if next to end
        if (heightMap[p.X, p.Y].Equals('z') && heightMap[p.Up.X, p.Up.Y].Equals('E')) return (true, new List<Point>() { p.Up });
        if (heightMap[p.X, p.Y].Equals('z') && heightMap[p.Left.X, p.Left.Y].Equals('E')) return (true, new List<Point>() { p.Left });
        if (heightMap[p.X, p.Y].Equals('z') && heightMap[p.Down.X, p.Down.Y].Equals('E')) return (true, new List<Point>() { p.Down });
        if (heightMap[p.X, p.Y].Equals('z') && heightMap[p.Right.X, p.Right.Y].Equals('E')) return (true, new List<Point>() { p.Right });

        // Look around (we should have not been there before)
        // Find next if higher or same
        var directionLengths = new List<List<Point>>();
        if (
            !path.Contains(p.Up) &&
            !IsOutOfBounds(p.Up) &&
            (
                heightMap[p.Up.X, p.Up.Y] == 'a' ||
                heightMap[p.Up.X, p.Up.Y] == heightMap[p.X, p.Y] ||
                heightMap[p.Up.X, p.Up.Y] == heightMap[p.X, p.Y] + 1
            )
        )
        {
            (var success, var childPath) = Find(p.Up, new List<Point>(path));
            if (success) directionLengths.Add(childPath);
        }

        if (
            !path.Contains(p.Left) &&
            !IsOutOfBounds(p.Left) &&
            (
                heightMap[p.Left.X, p.Left.Y] == 'a' ||
                heightMap[p.Left.X, p.Left.Y] == heightMap[p.X, p.Y] ||
                heightMap[p.Left.X, p.Left.Y] == heightMap[p.X, p.Y] + 1
            )
        )
        {
            (var success, var childPath) = Find(p.Left, new List<Point>(path));
            if (success) directionLengths.Add(childPath);
        }


        if (
            !path.Contains(p.Down) &&
            !IsOutOfBounds(p.Down) &&
            (
                heightMap[p.Down.X, p.Down.Y] == 'a' ||
                heightMap[p.Down.X, p.Down.Y] == heightMap[p.X, p.Y] ||
                heightMap[p.Down.X, p.Down.Y] == heightMap[p.X, p.Y] + 1
            )
        )
        {
            (var success, var childPath) = Find(p.Down, new List<Point>(path));
            if (success) directionLengths.Add(childPath);
        }

        if (
            !path.Contains(p.Right) &&
            !IsOutOfBounds(p.Right) &&
            (
                heightMap[p.Right.X, p.Right.Y] == 'a' ||
                heightMap[p.Right.X, p.Right.Y] == heightMap[p.X, p.Y] ||
                heightMap[p.Right.X, p.Right.Y] == heightMap[p.X, p.Y] + 1
            )
        )
        {
            (var success, var childPath) = Find(p.Right, new List<Point>(path));
            if (success) directionLengths.Add(childPath);
        }

        if (directionLengths.Count == 0)
        {
            return (false, new List<Point>());
        }

        // return the shortest
        var shortest = directionLengths.OrderBy(l => l.Count).First();
        return (true, shortest);
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
