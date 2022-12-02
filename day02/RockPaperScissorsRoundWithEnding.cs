public class RockPaperScissorsRoundWithEnding
{
    private char opponent;
    private char ending;

    public RockPaperScissorsRoundWithEnding(char opponent, char ending)
    {
        this.opponent = opponent;
        this.ending = ending;
    }

    public int MyScore
    {
        // Rock (A,X) -> Scissors (C,Z) -> Paper (B,Y) -> Rock (A,X)
        // X = 1
        // Y = 2
        // Z = 3

        // X = Loss (0)
        // Y = Draw (3)
        // Z = Win  (6)
        get
        {
            if (opponent == 'A' && ending == 'X') return (3 + 0);   // Me picks Z
            if (opponent == 'A' && ending == 'Y') return (1 + 3);   // Me picks X
            if (opponent == 'A' && ending == 'Z') return (2 + 6);   // Me picks Y

            if (opponent == 'B' && ending == 'X') return (1 + 0);   // Me picks X
            if (opponent == 'B' && ending == 'Y') return (2 + 3);   // Me picks Y
            if (opponent == 'B' && ending == 'Z') return (3 + 6);   // Me picks Z

            if (opponent == 'C' && ending == 'X') return (2 + 0);   // Me picks Y
            if (opponent == 'C' && ending == 'Y') return (3 + 3);   // Me picks Z
            if (opponent == 'C' && ending == 'Z') return (1 + 6);   // Me picks X

            return 0;
        }
    }
}