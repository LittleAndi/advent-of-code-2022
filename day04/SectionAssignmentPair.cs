using System.Text.RegularExpressions;

public class SectionAssignmentPair
{
    int[] elfAlfa;
    int[] elfBravo;
    public SectionAssignmentPair(string assignments)
    {
        var regex = new Regex(@"(\d+)-(\d+),(\d+)-(\d+)");
        var matches = regex.Match(assignments);
        var startAlfa = int.Parse(matches.Groups[1].Value);
        var endAlfa = int.Parse(matches.Groups[2].Value);
        var startBeta = int.Parse(matches.Groups[3].Value);
        var endBeta = int.Parse(matches.Groups[4].Value);

        elfAlfa = Enumerable.Range(startAlfa, endAlfa - startAlfa + 1).ToArray();
        elfBravo = Enumerable.Range(startBeta, endBeta - startBeta + 1).ToArray();
    }

    public bool OneAssignmentFullyContainsTheOther
    {
        get
        {
            if (elfAlfa.Except(elfBravo).Count() == 0) return true;
            if (elfBravo.Except(elfAlfa).Count() == 0) return true;
            return false;
        }
    }

    public bool AssignmentsOverlap
    {
        get
        {
            if (elfAlfa.Intersect(elfBravo).Count() > 0) return true;
            return false;
        }
    }
}