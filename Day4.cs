using System.Text.RegularExpressions;

namespace AoC2024;

public class Day4
{
    public void Part1()
    {
        var data = Utils.LoadData(4);

        List<string> lines = data.Split('\n').Select(x=>x.Trim()).ToList();

        int count = 0;

        var directions = new (int, int)[] { (1, 0), (-1, 0), (0, 1), (0,-1), (1, 1), (-1, 1), (1, -1), (-1, -1) };

        for (int x = 0; x < lines[0].Length; x++)
        {
            for (int y = 0; y < lines.Count; y++)
            {
                var pos = (x, y);

                foreach (var dir in directions)
                {
                    //Check all directions
                    if (IsMatch(lines, "XMAS", pos, p => (p.Item1 + dir.Item1, p.Item2 + dir.Item2)))
                    {
                        count++;
                    }
                }
            }
        }
        Console.WriteLine(count);
    }

    private bool IsMatch(List<string> grid, string word, (int, int) startPos, Func<(int, int), (int, int)> nextFunc)
    {
        for (int i = 0; i < word.Length; i++)
        {
            if (startPos.Item1 < 0 || startPos.Item1 >= grid[0].Length || startPos.Item2 < 0 || startPos.Item2 >= grid.Count)
            {
                return false;
            }

            if (grid[startPos.Item2][startPos.Item1] != word[i])
            {
                return false;
            }

            startPos = nextFunc(startPos);
        }

        return true;
    }

    public void Part2()
    {
        var data = Utils.LoadData(4);

        List<string> lines = data.Split('\n').Select(x=>x.Trim()).ToList();

        int count = 0;
        for (int y = 0; y < lines.Count; y++)
        {
            int x = lines[y].IndexOf("A");

            while (x != -1)
            {
                if (IsXmas(lines, x, y))
                {
                    count++;
                }
                x = lines[y].IndexOf("A", x + 1);
            }
        }

        Console.WriteLine(count);
    }

    private bool IsXmas(List<string> lines, int x, int y)
    {
        //Check bounds
        if (x == 0 || x == lines[0].Length-1 || y == 0 || y == lines.Count-1)
        {
            return false;
        }

        //Check top left/bottom right
        if ((lines[y-1][x-1] == 'M' && lines[y+1][x+1] == 'S') ||
            (lines[y-1][x-1] == 'S' && lines[y+1][x+1] == 'M'))
        {
            if ((lines[y-1][x+1] == 'M' && lines[y+1][x-1] == 'S') ||
                (lines[y-1][x+1] == 'S' && lines[y+1][x-1] == 'M'))
            {
                return true;
            }
        }

        return false;
    }
}