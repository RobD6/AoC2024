using System.Text.RegularExpressions;

namespace AoC2024;

public class Day6
{
    public void Part1()
    {
        var data = Utils.LoadData(6);

        (int, int) currentPos = (0, 0);
        HashSet<(int, int)> blockers = [];

        var lines = data.Split('\n').Select(line => line.Trim()).ToList();

        int width = lines[0].Length;
        int height = lines.Count();

        for (int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] == '#')
                {
                    blockers.Add((x, y));
                }
                else if (lines[y][x] == '^')
                {
                    currentPos = (x, y);
                }
            }
        }

        HashSet<(int,int)> visited = [];
        visited.Add(currentPos);
        var currentDir = (0, -1);

        while (true)
        {
            (int, int) nextPos = (currentPos.Item1 + currentDir.Item1, currentPos.Item2 + currentDir.Item2);
            while (blockers.Contains(nextPos))
            {
                currentDir = TurnRight(currentDir);
                nextPos = (currentPos.Item1 + currentDir.Item1, currentPos.Item2 + currentDir.Item2);
            }

            if (nextPos.Item1 >= width || nextPos.Item1 < 0 || nextPos.Item2 >= height || nextPos.Item2 < 0)
            {
                break;
            }

            currentPos = nextPos;
            visited.Add(currentPos);
        }

        Console.WriteLine(visited.Count);
    }

    private (int, int) TurnRight((int, int) dir)
    {
        switch (dir)
        {
            case (0, 1):
                return (-1, 0);
            case (-1, 0):
                return (0, -1);
            case (0, -1):
                return (1, 0);
            case (1, 0):
                return (0, 1);
        }

        throw new System.Exception("Invalid direction");
    }

    public void Part2()
    {
        var data = Utils.LoadData(6);
    }
}