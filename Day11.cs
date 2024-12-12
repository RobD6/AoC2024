using System.Text.RegularExpressions;

namespace AoC2024;

public class Day11
{
    public void Part1()
    {
        var data = Utils.LoadData(11).Trim();

        List<long> stones = data.Split(' ').Select(long.Parse).ToList();

        for (long i = 0; i < 25; i++)
        {
            stones = Blink(ref stones);
        }

        Console.WriteLine(stones.Count);
    }

    private static List<long> Blink(ref List<long> stones)
    {
        List<long> newStones = [];

        foreach (var stone in stones)
        {
            if (stone == 0)
            {
                newStones.Add(1);
                continue;
            }
            string s = stone.ToString();
            if (s.Length % 2 == 0)
            {
                int half = s.Length / 2;
                string firstHalf = s.Substring(0, half);
                string secondHalf = s.Substring(half);
                newStones.Add(long.Parse(firstHalf));
                newStones.Add(long.Parse(secondHalf));
                continue;
            }

            newStones.Add(stone * 2024);
        }

        return newStones;
    }

    private Dictionary<long, long> BlinkSets(Dictionary<long, long> stoneDict)
    {
        Dictionary<long, long> newDict = new();

        foreach ((long stone, long count) in stoneDict)
        {
            if (stone == 0)
            {
                newDict.TryAdd(1, 0);
                newDict[1] += count;
                continue;
            }
            string s = stone.ToString();
            if (s.Length % 2 == 0)
            {
                int half = s.Length / 2;
                long firstHalf = long.Parse(s.Substring(0, half));
                long secondHalf = long.Parse(s.Substring(half));

                newDict.TryAdd(firstHalf, 0);
                newDict.TryAdd(secondHalf, 0);
                newDict[firstHalf] += count;
                newDict[secondHalf] += count;
                continue;
            }

            newDict.TryAdd(stone * 2024, 0);
            newDict[stone * 2024] += count;
        }

        return newDict;
    }

    public void Part2()
    {
        var data = Utils.LoadData(11).Trim();

        List<long> stones = data.Split(' ').Select(long.Parse).ToList();

        Dictionary<long, long> stoneSets = new();

        foreach (var stone in stones)
        {
            stoneSets.TryAdd(stone, 0);

            stoneSets[stone]++;
        }

        for (int i = 0; i < 75; i++)
        {
            stoneSets = BlinkSets(stoneSets);
        }

        long sum = stoneSets.Values.Sum();
        Console.WriteLine(sum);
    }
}