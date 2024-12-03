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

    public class ControlWordPosition
    {
        public int Position;
        public bool Enable;
    }

    public void Part2()
    {
        var data = Utils.LoadData(3);

        string enableString = "do()";
        string disableString = "don't()";
        List<ControlWordPosition> controlWordPositions = new List<ControlWordPosition>();

        int index = -1;
        while ((index = data.IndexOf(enableString, index + 1)) != -1)
        {
            controlWordPositions.Add(new ControlWordPosition(){Enable = true, Position = index});
        }

        index = -1;
        while ((index = data.IndexOf(disableString, index + 1)) != -1)
        {
            controlWordPositions.Add(new ControlWordPosition(){Enable = false, Position = index});
        }

        controlWordPositions.Sort((a, b) => a.Position.CompareTo(b.Position));

        Regex mulRegex = new Regex(@"mul\((\d+),(\d+)\)");

        int sum = 0;
        foreach (Match match in mulRegex.Matches(data))
        {
            //Find the last control word that is before the match
            ControlWordPosition controlWord = controlWordPositions.LastOrDefault(c => c.Position < match.Index);
            if (controlWord != null && !controlWord.Enable)
            {
                continue;
            }

            sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

        Console.WriteLine(sum);
    }

}