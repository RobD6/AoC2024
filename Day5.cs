using System.Text.RegularExpressions;

namespace AoC2024;

public class Day5
{
    public void Part1()
    {
        var data = Utils.LoadData(5);
        var lines = data.Split('\n').Select(line => line.Trim()).ToList();
        Dictionary<int, List<int>> orderDict = new();

        var lineNum = ParseOrderRules(lines, orderDict);

        lineNum++;
        int sum = 0;

        for (; lineNum < lines.Count; lineNum++)
        {
            List<int> pages = lines[lineNum].Split(',').Select(int.Parse).ToList();

            var isValid = IsValid(pages, orderDict);

            //Add the middle page number to the sum
            if (isValid)
            {
                sum += pages[(pages.Count - 1) / 2];
            }
        }

        Console.WriteLine(sum);
    }

    Dictionary<int, List<int>> OrderDict = new();
    public void Part2()
    {
        var data = Utils.LoadData(5);
        var lines = data.Split('\n').Select(line => line.Trim()).ToList();

        var lineNum = ParseOrderRules(lines, OrderDict);

        lineNum++;
        int sum = 0;

        for (; lineNum < lines.Count; lineNum++)
        {
            List<int> pages = lines[lineNum].Split(',').Select(int.Parse).ToList();

            var isValid = IsValid(pages, OrderDict);

            //Add the middle page number to the sum
            if (!isValid)
            {
                pages.Sort(ComparePages);
                sum += pages[(pages.Count - 1) / 2];
            }
        }

        Console.WriteLine(sum);
    }

    private int ComparePages(int x, int y)
    {
        if (OrderDict.ContainsKey(x))
        {
            if (OrderDict[x].Contains(y))
            {
                return -1;
            }
        }
        if (OrderDict.ContainsKey(y))
        {
            if (OrderDict[y].Contains(x))
            {
                return 1;
            }
        }

        return 0;
    }

    private static bool IsValid(List<int> pages, Dictionary<int, List<int>> orderDict)
    {
        var seenNums = new HashSet<int>();
        bool isValid = true;

        foreach (int page in pages)
        {
            seenNums.Add(page);
            //Validate the rule for this page
            if (orderDict.ContainsKey(page))
            {
                foreach (int otherPage in orderDict[page])
                {
                    if (pages.Contains(otherPage) && !seenNums.Contains(otherPage))
                    {
                        isValid = false;
                        break;
                    }
                }

                if (!isValid)
                {
                    break;
                }
            }
        }

        return isValid;
    }

    private static int ParseOrderRules(List<string> lines, Dictionary<int, List<int>> orderDict)
    {
        int lineNum;
        for (lineNum = 0; lines[lineNum].Length > 0; lineNum++)
        {
            var orderPair = lines[lineNum].Split('|');

            int pageKey = int.Parse(orderPair[1]);
            if (!orderDict.ContainsKey(pageKey))
            {
                orderDict[pageKey] = new List<int>();
            }
            orderDict[pageKey].Add(int.Parse(orderPair[0]));
        }

        return lineNum;
    }


}