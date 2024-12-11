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

    public void Part2()
    {
        var data = Utils.LoadData(11).Trim();

        List<long> stones = data.Split(' ').Select(long.Parse).ToList();

        for (long i = 0; i < 75; i++)
        {
            stones = Blink(ref stones);
        }

        Console.WriteLine(stones.Count);
    }
}