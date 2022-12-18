using System.Diagnostics.CodeAnalysis;

var heightMapInfo = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => l.ToCharArray())
    .ToArray();

var heightMap = new HeightMap(heightMapInfo);

//System.Console.WriteLine(heightMap.ToString());

System.Console.WriteLine(heightMap.ClimbFromStart());
System.Console.WriteLine(heightMap.ClimbFromA());

public class HeightMap
{
    char[,] heightMap;
    int xSize;
    int ySize;
    Point start;
    Point goal;
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
                if (heightMap[x, y].Equals('E')) goal = new Point(x, y);
            }
        }
    }

    public int ClimbFromStart()
    {
        return Climb(start);
    }

    public int ClimbFromA()
    {
        int bestSteps = int.MaxValue;
        for (int y = 0; y < heightMap.GetLength(1); y++)
        {
            for (int x = 0; x < heightMap.GetLength(0); x++)
            {
                if (heightMap[x, y] == 'S' || heightMap[x, y] == 'a')
                {
                    int steps = Climb(new Point(x, y));
                    if (steps < bestSteps) bestSteps = steps;
                }
            }
        }
        return bestSteps;
    }

    public int Climb(Point root)
    {
        var bestSteps = int.MaxValue;
        var explored = new HashSet<Point>();
        var path = new Dictionary<Point, Point?>();
        var Q = new Queue<(Point, int)>();
        explored.Add(root);
        Q.Enqueue((root, 0));
        path.Add(root, null);

        while (Q.Count > 0)
        {
            (var v, int steps) = Q.Dequeue();
            // System.Console.WriteLine($"{v} {heightMap[v.X, v.Y]} {steps}, still in queue {Q.Count}");
            if (v.Equals(goal))
            {
                if (steps < bestSteps) bestSteps = steps;
                char[,] queueMap = new char[xSize, ySize];
                var currentPoint = (Point)path.Last().Key;
                var count = 0;
                while (!currentPoint.Equals(root))
                {
                    queueMap[currentPoint.X, currentPoint.Y] = 'x';
                    count++;
                    currentPoint = path[currentPoint].Value;
                    // PrintMap(queueMap);
                }
                continue;
            }

            var adjacentEdges = new List<Point> { v.Up, v.Left, v.Down, v.Right };

            foreach (var w in adjacentEdges)
            {
                if (IsOutOfBounds(w) || explored.Contains(w)) continue;
                var vValue = heightMap[v.X, v.Y];
                var wValue = heightMap[w.X, w.Y];
                if (v.Equals(start)) vValue = 'a'; // override start
                if (w.Equals(goal)) wValue = 'z'; // override end
                var climbOneOrLower = (wValue <= vValue + 1);
                if (climbOneOrLower)
                {
                    explored.Add(w);
                    path.Add(w, v);
                    Q.Enqueue((w, steps + 1));
                }
            }
        }

        return bestSteps;
    }

    private void PrintMap(char[,] queueMap)
    {
        for (int y = 0; y < queueMap.GetLength(1); y++)
        {
            System.Console.WriteLine(new string(Enumerable.Range(0, queueMap.GetLength(0)).Select(x => queueMap[x, y]).ToArray()));
        }
        System.Console.WriteLine();
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
