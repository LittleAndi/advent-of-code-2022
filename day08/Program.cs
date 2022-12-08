using System.Text.RegularExpressions;

var treeMapInfo = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => l.ToCharArray())
    .ToArray();

var treeMap = new TreeMap(treeMapInfo);

System.Console.WriteLine(treeMap.VisibleTrees);
System.Console.WriteLine(treeMap.BestScenicScore);

public class TreeMap
{
    int[,] treeMap;
    int xMin = 0;
    int xSize;
    int yMin = 0;
    int ySize;
    public TreeMap(char[][] mapInput)
    {
        xSize = mapInput[0].Length;
        ySize = mapInput.Length;

        treeMap = new int[xSize, ySize];

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                treeMap[x, y] = Convert.ToInt32(mapInput[y][x].ToString());
            }
        }
    }
    public bool TopVisible(int x, int y)
    {
        if (y == 0) return true;
        for (int yPos = 0; yPos < y; yPos++)
        {
            if (treeMap[x, yPos] >= treeMap[x, y]) return false;
        }
        return true;
    }

    public bool BottomVisible(int x, int y)
    {
        if (y == ySize - 1) return true;
        for (int yPos = y + 1; yPos < ySize; yPos++)
        {
            if (treeMap[x, yPos] >= treeMap[x, y]) return false;
        }
        return true;
    }

    public bool LeftVisible(int x, int y)
    {
        if (x == 0) return true;
        for (int xPos = 0; xPos < x; xPos++)
        {
            if (treeMap[xPos, y] >= treeMap[x, y]) return false;
        }
        return true;
    }

    public bool RightVisible(int x, int y)
    {
        if (x == xSize - 1) return true;
        for (int xPos = x + 1; xPos < xSize; xPos++)
        {
            if (treeMap[xPos, y] >= treeMap[x, y]) return false;
        }
        return true;
    }

    public int TopVisibleCount(int x, int y)
    {
        int visibleTrees = 0;
        int yPos = y - 1;
        int xPos = x;
        while (yPos >= 0)
        {
            visibleTrees++;
            if (treeMap[xPos, yPos] >= treeMap[x, y]) break;
            yPos--;
        }
        return visibleTrees;
    }

    public int BottomVisibleCount(int x, int y)
    {
        int visibleTrees = 0;
        int yPos = y + 1;
        int xPos = x;
        while (yPos < ySize)
        {
            visibleTrees++;
            if (treeMap[xPos, yPos] >= treeMap[x, y]) break;
            yPos++;
        }
        return visibleTrees;
    }

    public int LeftVisibleCount(int x, int y)
    {
        int visibleTrees = 0;
        int yPos = y;
        int xPos = x - 1;
        while (xPos >= 0)
        {
            visibleTrees++;
            if (treeMap[xPos, yPos] >= treeMap[x, y]) break;
            xPos--;
        }
        return visibleTrees;
    }

    public int RightVisibleCount(int x, int y)
    {
        int visibleTrees = 0;
        int yPos = y;
        int xPos = x + 1;
        while (xPos < xSize)
        {
            visibleTrees++;
            if (treeMap[xPos, yPos] >= treeMap[x, y]) break;
            xPos++;
        }
        return visibleTrees;
    }

    public int VisibleTrees
    {
        get
        {
            int treesVisible = 0;
            for (int yPos = 0; yPos < ySize; yPos++)
            {
                for (int xPos = 0; xPos < xSize; xPos++)
                {
                    if (
                        TopVisible(xPos, yPos) ||
                        BottomVisible(xPos, yPos) ||
                        LeftVisible(xPos, yPos) ||
                        RightVisible(xPos, yPos)
                    ) treesVisible++;
                }

            }
            return treesVisible;
        }
    }

    public int ScenicScore(int x, int y)
    {
        return TopVisibleCount(x, y) * LeftVisibleCount(x, y) * BottomVisibleCount(x, y) * RightVisibleCount(x, y);
    }

    public int BestScenicScore
    {
        get
        {
            int bestScenicScore = 0;
            for (int yPos = 1; yPos < ySize - 1; yPos++)
            {
                for (int xPos = 1; xPos < xSize - 1; xPos++)
                {
                    var scenicScore = ScenicScore(xPos, yPos);
                    if (scenicScore > bestScenicScore) bestScenicScore = scenicScore;
                }

            }
            return bestScenicScore;
        }
    }
}