var distressSignals = File.ReadAllLines("input.txt")
    .ToList();

System.Console.WriteLine(distressSignals[0]);
System.Console.WriteLine(distressSignals[1]);



public class SignalDecoder
{
    private readonly string l;
    private readonly string r;

    public SignalDecoder(string left, string right)
    {
        this.l = left;
        this.r = right;
    }

    public bool InTheRightOrder
    {
        get
        {
            Stack<Thing> lStack = new Stack<Thing>();
            Stack<Thing> rStack = new Stack<Thing>();

            int lPos = 0;
            int rPos = 0;

            var t = l.Split(new char[] { '[', ']', ',' });

            return l == r;
        }
    }

    public class Thing
    {
        private readonly string thing;

        public Thing(string thing)
        {
            this.thing = thing;
        }
    }
}