var sectionAssignments = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => new SectionAssignmentPair(l))
    .ToList();

System.Console.WriteLine(sectionAssignments.Where(sa => sa.OneAssignmentFullyContainsTheOther).Count());

System.Console.WriteLine(sectionAssignments.Where(sa => sa.AssignmentsOverlap).Count());
