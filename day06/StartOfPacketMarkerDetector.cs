public class StartOfPacketMarkerDetector
{
    private string signal;

    public StartOfPacketMarkerDetector(string signal)
    {
        this.signal = signal;
    }

    public int GetMarkerEnd(int markerSize)
    {
        var markerEnd = markerSize;
        while (markerEnd <= signal.Length)
        {
            var markerStart = markerEnd - markerSize;
            var marker = signal[markerStart..markerEnd];
            var markerUnique = marker.ToHashSet<char>();
            if (markerUnique.Count == markerSize) break;
            markerEnd++;
        }
        return markerEnd;
    }
}