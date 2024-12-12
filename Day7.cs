using System.Text.RegularExpressions;

namespace AoC2024;

public class Day7
{
    public void Part1()
    {
        var data = Utils.LoadData(7);

        var lines = data.Split('\n').Select(line => line.Trim()).ToList();

        long sum = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(':');
            long target = long.Parse(parts[0]);
            var componentStrings = parts[1].Trim().Split(' ');
            var components = componentStrings.Select(long.Parse).ToList();
            components.Reverse();   //Since we're evaluating right to left, not left to right

            foreach (long eval in EvaluateTree(components))
            {
                if (eval == target)
                {
                    sum += target;
                    break;
                }
            }
        }

        Console.WriteLine(sum);
    }

    private IEnumerable<long> EvaluateTree(List<long> components, int startIndex = 0)
    {
        if (startIndex == components.Count-1)
        {
            yield return components[^1];
        }
        else
        {
            foreach (long sum in EvaluateTree(components, startIndex + 1))
            {
                yield return components[startIndex] + sum;
                yield return components[startIndex] * sum;
            }
        }
    }

    public void Part2()
    {
        var data = Utils.LoadData(7);

        var lines = data.Split('\n').Select(line => line.Trim()).ToList();

        long sum = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(':');
            long target = long.Parse(parts[0]);
            var componentStrings = parts[1].Trim().Split(' ');
            var components = componentStrings.Select(long.Parse).ToList();
            components.Reverse();

            foreach (long eval in EvaluateTree2(components))
            {
                if (eval == target)
                {
                    sum += target;
                    break;
                }
            }
        }

        Console.WriteLine(sum);
    }

    private IEnumerable<long> EvaluateTree2(List<long> components, int startIndex = 0)
    {
        if (startIndex == components.Count-1)
        {
            yield return components[^1];
        }
        else
        {
            foreach (long sum in EvaluateTree2(components, startIndex + 1))
            {
                yield return components[startIndex] + sum;
                yield return components[startIndex] * sum;

                long order1 = (long)Math.Floor(Math.Log10(components[startIndex])) + 1;
                yield return components[startIndex] + (long)Math.Pow(10, order1) * sum;
            }
        }
    }
}