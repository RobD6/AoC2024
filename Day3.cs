using System.Text.RegularExpressions;

namespace AoC2024;

public class Day3
{
    public void Part1()
    {
        var data = Utils.LoadData(3);

        Regex mulRegex = new Regex(@"mul\((\d+),(\d+)\)");

        int sum = 0;
        foreach (Match match in mulRegex.Matches(data))
        {
            sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

        Console.WriteLine(sum);
    }

    public void Part2()
    {
        var data = Utils.LoadData(3);

    }

}