using System.Text.RegularExpressions;

namespace AoC2024;

public class Day2
{
    public void Part1()
    {
        var data = Utils.LoadData(2);

        int safeLines = 0;
        foreach (var line in data.Split('\n'))
        {
            List<int> nums = line.Split(' ').Select(int.Parse).ToList();

            if (IsSafeSequence(nums))
            {
                safeLines++;
            }
        }
        Console.WriteLine(safeLines);
    }

    public void Part2()
    {
        var data = Utils.LoadData(2);

        int safeLines = 0;
        foreach (var line in data.Split('\n'))
        {
            List<int> nums = line.Split(' ').Select(int.Parse).ToList();

            if (IsSafeSequence(nums))
            {
                safeLines++;
            }
            else
            {
                for (int i = 0; i < nums.Count; i++)
                {
                    var newSeq = new List<int>(nums);
                    newSeq.RemoveAt(i);
                    if (IsSafeSequence(newSeq))
                    {
                        safeLines++;
                        break;
                    }
                }
            }
        }

        Console.WriteLine(safeLines);
    }


    private bool IsSafeSequence(List<int> seq)
    {
        int sign = Math.Sign(seq[1] - seq[0]);

        bool isSafe = true;
        for (int i = 0; i < seq.Count - 1; i++)
        {
            int val = seq[i + 1] - seq[i];

            if (Math.Sign(val) != sign ||
                Math.Abs(val) is < 1 or > 3)
            {
                isSafe = false;
            }
        }

        return isSafe;
    }
}