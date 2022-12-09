using System.Diagnostics.CodeAnalysis;

var movements = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .ToList();

System.Console.WriteLine(movements.Count);

var rope = new Rope();

var tailPositions = new List<Point>();
foreach (var movement in movements)
{
    tailPositions.AddRange(rope.MoveHead(movement));
}

System.Console.WriteLine($"Unique tailpositions: {tailPositions.ToHashSet().Count}");

public class Rope
{
    Point Head;
    public Point Tail;
    public Rope()
    {
        Head = new Point() { X = 0, Y = 0 };
        Tail = new Point() { X = 0, Y = 0 };
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
                    Head = Head.Up;
                    if (!Tail.IsOnOrNextTo(Head))
                    {
                        (var dX, var dY) = Head.Distance(Tail);
                        if (Math.Sign(dY) == -1) Tail = Tail.Up;
                        if (Math.Sign(dX) == -1) Tail = Tail.Left;
                        if (Math.Sign(dY) == 1) Tail = Tail.Down;
                        if (Math.Sign(dX) == 1) Tail = Tail.Right;
                    }
                    tailPositions.Add(Tail);
                }
                break;
            case 'L':
                for (int i = 0; i < distance; i++)
                {
                    Head = Head.Left;
                    if (!Tail.IsOnOrNextTo(Head))
                    {
                        (var dX, var dY) = Head.Distance(Tail);
                        if (Math.Sign(dY) == -1) Tail = Tail.Up;
                        if (Math.Sign(dX) == -1) Tail = Tail.Left;
                        if (Math.Sign(dY) == 1) Tail = Tail.Down;
                        if (Math.Sign(dX) == 1) Tail = Tail.Right;
                    }
                    tailPositions.Add(Tail);
                }
                break;
            case 'D':
                for (int i = 0; i < distance; i++)
                {
                    Head = Head.Down;
                    if (!Tail.IsOnOrNextTo(Head))
                    {
                        (var dX, var dY) = Head.Distance(Tail);
                        if (Math.Sign(dY) == -1) Tail = Tail.Up;
                        if (Math.Sign(dX) == -1) Tail = Tail.Left;
                        if (Math.Sign(dY) == 1) Tail = Tail.Down;
                        if (Math.Sign(dX) == 1) Tail = Tail.Right;
                    }
                    tailPositions.Add(Tail);
                }
                break;
            case 'R':
                for (int i = 0; i < distance; i++)
                {
                    Head = Head.Right;
                    if (!Tail.IsOnOrNextTo(Head))
                    {
                        (var dX, var dY) = Head.Distance(Tail);
                        if (Math.Sign(dY) == -1) Tail = Tail.Up;
                        if (Math.Sign(dX) == -1) Tail = Tail.Left;
                        if (Math.Sign(dY) == 1) Tail = Tail.Down;
                        if (Math.Sign(dX) == 1) Tail = Tail.Right;
                    }
                    tailPositions.Add(Tail);
                }
                break;
        }
        return tailPositions;
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
