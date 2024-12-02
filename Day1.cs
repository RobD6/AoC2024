using System.Text.RegularExpressions;

namespace AoC2024;

public class Day1
{
    public void Part1()
    {
        var data = Utils.LoadData(1);

        List<int> columnA = new();
        List<int> columnB = new();

        foreach (var line in data.Split('\n'))
        {
            Regex regex = new(@"(\d+)[ ]*(\d+)");
            var match = regex.Match(line);
            if (match.Success)
            {
                columnA.Add(int.Parse(match.Groups[1].Value));
                columnB.Add(int.Parse(match.Groups[2].Value));
            }
        }

        columnA.Sort();
        columnB.Sort();

        int result = columnA.Zip(columnB, (a, b) => Math.Abs(a - b)).Sum();
        Console.WriteLine(result);
    }

    public void Part2()
    {
        var data = Utils.LoadData(1);

        List<int> columnA = [];
        Dictionary<int, int> columnBDict = new();

        foreach (var line in data.Split('\n'))
        {
            Regex regex = new(@"(\d+)[ ]*(\d+)");
            var match = regex.Match(line);
            if (match.Success)
            {
                columnA.Add(int.Parse(match.Groups[1].Value));
                int columnB = int.Parse(match.Groups[2].Value);
                columnBDict[columnB] = columnBDict.GetValueOrDefault(columnB, 0) + 1;
            }
        }

        int sum = columnA.Where(val => columnBDict.ContainsKey(val)).
            Sum(val => val * columnBDict[val]);

        Console.WriteLine(sum);
    }
}