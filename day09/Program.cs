using System.Diagnostics.CodeAnalysis;

var movements = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .ToList();

System.Console.WriteLine(movements.Count);

var shortRope = new Rope(1);

var tailPositionsForShortRope = new List<Point>();
foreach (var movement in movements)
{
    tailPositionsForShortRope.AddRange(shortRope.MoveHead(movement));
}

System.Console.WriteLine($"Part 1 - Unique tailpositions: {tailPositionsForShortRope.ToHashSet().Count}");

var longRope = new Rope(9);
var tailPositionsForLongRope = new List<Point>();
foreach (var movement in movements)
{
    tailPositionsForLongRope.AddRange(longRope.MoveHead(movement));
}

System.Console.WriteLine($"Part 2 - Unique tailpositions: {tailPositionsForLongRope.ToHashSet().Count}");

public class Rope
{
    public List<Point> Knots;
    public Rope(int knotCountExcludingHead = 1)
    {
        Knots = new List<Point>();
        Knots.Add(new Point() { X = 0, Y = 0 });   // Head
        for (int knots = 0; knots < knotCountExcludingHead; knots++)
        {
            Knots.Add(new Point() { X = 0, Y = 0 });
        }
    }

    public IEnumerable<Point> MoveHead(string instruction)
    {
        var direction = instruction[0];
        var distance = int.Parse(instruction[2..]);

        var tailPositions = new HashSet<Point>();
        switch (direction)
        {
            case 'U':
                for (int i = 0; i < distance; i++)
                {
                    Knots[0] = Knots[0].Up; // Head
                    MoveKnots();
                    tailPositions.Add(Knots.Last());
                }
                break;
            case 'L':
                for (int i = 0; i < distance; i++)
                {
                    Knots[0] = Knots[0].Left; // Head
                    MoveKnots();
                    tailPositions.Add(Knots.Last());
                }
                break;
            case 'D':
                for (int i = 0; i < distance; i++)
                {
                    Knots[0] = Knots[0].Down; // Head
                    MoveKnots();
                    tailPositions.Add(Knots.Last());
                }
                break;
            case 'R':
                for (int i = 0; i < distance; i++)
                {
                    Knots[0] = Knots[0].Right; // Head
                    MoveKnots();
                    tailPositions.Add(Knots.Last());
                }
                break;
        }
        return tailPositions;
    }

    private void MoveKnots()
    {
        for (int knot = 1; knot < Knots.Count; knot++)
        {
            if (!Knots[knot].IsOnOrNextTo(Knots[knot - 1]))
            {
                (var dX, var dY) = Knots[knot - 1].Distance(Knots[knot]);
                if (Math.Sign(dY) == -1) Knots[knot] = Knots[knot].Up;
                if (Math.Sign(dX) == -1) Knots[knot] = Knots[knot].Left;
                if (Math.Sign(dY) == 1) Knots[knot] = Knots[knot].Down;
                if (Math.Sign(dX) == 1) Knots[knot] = Knots[knot].Right;
            }
        }
    }
}
public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Risk { get; set; }
    public Point Up => new Point { X = X, Y = Y - 1 };
    public Point UpLeft => new Point { X = X - 1, Y = Y - 1 };
    public Point Left => new Point { X = X - 1, Y = Y };
    public Point DownLeft => new Point { X = X - 1, Y = Y + 1 };
    public Point Down => new Point { X = X, Y = Y + 1 };
    public Point DownRight => new Point { X = X + 1, Y = Y + 1 };
    public Point Right => new Point { X = X + 1, Y = Y };
    public Point UpRight => new Point { X = X + 1, Y = Y - 1 };

    public bool IsOnOrNextTo(Point other)
    {
        return
            other.Equals(this) ||
            other.Equals(Up) ||
            other.Equals(UpLeft) ||
            other.Equals(Left) ||
            other.Equals(DownLeft) ||
            other.Equals(Down) ||
            other.Equals(DownRight) ||
            other.Equals(Right) ||
            other.Equals(UpRight);
    }

    public (int dX, int dY) Distance(Point other)
    {
        return (X - other.X, Y - other.Y);
    }

    public override bool Equals([NotNullWhen(true)] object obj) => (((Point)obj).X == this.X && ((Point)obj).Y == this.Y);

    public override int GetHashCode()
    {
        return (X, Y).GetHashCode();
    }
}
