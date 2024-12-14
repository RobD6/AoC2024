using System.Text.RegularExpressions;

namespace AoC2024;

public class Day14
{
    public void Part1()
    {
        var data = Utils.LoadData(14).Trim();

        var robots = ParseRobots(data);
        int width = 101;
        int height = 103;
        // int width = 11;
        // int height = 7;

        StepRobots(robots, 100, width, height);

        // PrintRobots(robots, width, height);

        //Get quadrants
        int xMiddle = width / 2;
        int yMiddle = height / 2;

        int[] quadCounts = [0, 0, 0, 0];
        foreach (var robot in robots)
        {
            if (robot.x < xMiddle)
            {
                if (robot.y < yMiddle)
                {
                    quadCounts[0]++;
                }
                else if (robot.y > yMiddle)
                {
                    quadCounts[2]++;
                }
            }
            else if (robot.x > xMiddle)
            {
                if (robot.y < yMiddle)
                {
                    quadCounts[1]++;
                }
                else if (robot.y > yMiddle)
                {
                    quadCounts[3]++;
                }
            }
        }

        Console.WriteLine((quadCounts[0] * quadCounts[1] * quadCounts[2] * quadCounts[3]));
    }

    private void StepRobots(List<Robot> robots, int steps, int width, int height)
    {
        foreach (var robot in robots)
        {
            robot.x += robot.vx * steps;
            robot.y += robot.vy * steps;

            robot.x = Wrap(robot.x, width);
            robot.y = Wrap(robot.y, height);
        }
    }

    private List<Robot> ParseRobots(string data)
    {
        List<Robot> robots = new();

        //p=95,29 v=-82,-32
        Regex reg = new Regex(@"p=(\d+),(\d+) v=(-?\d+),(-?\d+)");

        foreach (Match match in reg.Matches(data))
        {
            Robot robot = new()
            {
                x = int.Parse(match.Groups[1].Value),
                y = int.Parse(match.Groups[2].Value),
                vx = int.Parse(match.Groups[3].Value),
                vy = int.Parse(match.Groups[4].Value),
            };

            robots.Add(robot);
        }

        return robots;
    }

    private bool PrintRobots(List<Robot> robots, int width, int height, int step = 1)
    {
        int contiguous = 0;
        HashSet<(int, int)> positions = [];
        foreach (var robot in robots)
        {
            positions.Add((robot.x, robot.y));
        }

        bool lastWasRobot = false;
        for (int y = 0; y < height; y+=step)
        {
            string line = "";
            for (int x = 0; x < width; x++)
            {
                bool isRobot = positions.Contains((x, y));
                line += isRobot ? "*" : ".";
                if (isRobot && lastWasRobot)
                {
                    contiguous++;
                }

                lastWasRobot = isRobot;
            }
            Console.WriteLine(line);
        }

        return contiguous > 30;
    }

    public static int Wrap(int x, int n)
    {
        return ((x % n) + n) % n;
    }

    class Robot
    {
        public int x;
        public int y;
        public int vx;
        public int vy;
    }

    public void Part2()
    {
        var data = Utils.LoadData(14).Trim();

        var robots = ParseRobots(data);
        int width = 101;
        int height = 103;
        // int width = 11;
        // int height = 7;

        // PrintRobots(robots, width, height);
        int seconds = 0;
        while (true)
        {
            StepRobots(robots, 1, width, height);
            seconds++;
            bool suspect = PrintRobots(robots, width, height, 3);
            Console.WriteLine(seconds);

            if (suspect)
            {
                PrintRobots(robots, width, height, 1);
                Console.ReadKey();
            }
        }
    }
}