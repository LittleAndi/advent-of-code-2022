public class RockPaperScissorsRound
{
    private char opponent;
    private char me;

    public RockPaperScissorsRound(char opponent, char me)
    {
        this.opponent = opponent;
        this.me = me;
    }
    public long MyScore
    {
        get
        {
            // Me Wins
            if (opponent == 'A' && me == 'Y') return (2 + 6);
            if (opponent == 'B' && me == 'Z') return (3 + 6);
            if (opponent == 'C' && me == 'X') return (1 + 6);

            // Draws
            if (opponent == 'A' && me == 'X') return (1 + 3);
            if (opponent == 'B' && me == 'Y') return (2 + 3);
            if (opponent == 'C' && me == 'Z') return (3 + 3);

            // Losses
            if (me == 'X') return 1;
            if (me == 'Y') return 2;
            if (me == 'Z') return 3;

            return 0;
        }
    }
}