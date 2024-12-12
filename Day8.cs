using System.Text.RegularExpressions;

namespace AoC2024;

public class Day8
{
    public void Part1()
    {
        var data = Utils.LoadData(8);
        var map = ParseMap(data, out int width, out int height);

        HashSet<(int, int)> antennaPosMap = [];

        foreach (var key in map.Keys)
        {
            foreach (var first in map[key])
            {
                foreach (var second in map[key])
                {
                    if (first == second)
                    {
                        continue;
                    }

                    var diff = (first.Item1 - second.Item1, first.Item2 - second.Item2);
                    var pos = (first.Item1 + diff.Item1, first.Item2 + diff.Item2);

                    if (pos.Item1 >= 0 && pos.Item1 < width &&
                        pos.Item2 >= 0 && pos.Item2 < height)
                    {
                        antennaPosMap.Add(pos);
                    }
                }
            }
        }
        Console.WriteLine(antennaPosMap.Count);
    }

    Dictionary<char, List<(int, int)>> ParseMap(string data, out int width, out int height)
    {
        Dictionary<char, List<(int, int)>> map = new();

        var lines = data.Split('\n').Select(line => line.Trim()).ToList();
        height = lines.Count;
        width = lines[0].Length;

        for (int y = 0; y < lines.Count; y++)
        {
            var line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                char cr = line[x];
                if (cr != '.')
                {
                    if (!map.ContainsKey(cr))
                    {
                        map[cr] = [];
                    }

                    map[cr].Add((x, y));
                }
            }
        }

        return map;
    }

    public void Part2()
    {
        var data = Utils.LoadData(8);
        var map = ParseMap(data, out int width, out int height);

        HashSet<(int, int)> antennaPosMap = [];

        foreach (var key in map.Keys)
        {
            foreach (var first in map[key])
            {
                foreach (var second in map[key])
                {
                    if (first == second)
                    {
                        continue;
                    }

                    var diff = (first.Item1 - second.Item1, first.Item2 - second.Item2);

                    int gcf = Utils.Gcf(Math.Abs(diff.Item1), Math.Abs(diff.Item2));
                    var offset = (diff.Item1 / gcf, diff.Item2 / gcf);

                    //Make sure we do the bit in between (bit inefficient - both directions will do this)
                    var pos = second;

                    while (pos.Item1 >= 0 && pos.Item1 < width &&
                        pos.Item2 >= 0 && pos.Item2 < height)
                    {
                        antennaPosMap.Add(pos);
                        pos = (pos.Item1 + offset.Item1, pos.Item2 + offset.Item2);
                    }
                }
            }
        }
        Console.WriteLine(antennaPosMap.Count);
    }

}