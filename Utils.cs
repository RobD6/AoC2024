namespace AoC2024;

public static class Utils
{
    public static string LoadData(int day)
    {
        return System.IO.File.ReadAllText($"Data/Day{day}.txt");
    }

    static public int Gcf(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}