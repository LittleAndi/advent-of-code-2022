var signal = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .First();

var markerDetector = new StartOfPacketMarkerDetector(signal);
System.Console.WriteLine(markerDetector.GetMarkerEnd(4));
System.Console.WriteLine(markerDetector.GetMarkerEnd(14));
