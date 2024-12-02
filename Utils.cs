namespace AoC2024;

public static class Utils
{
    public static string LoadData(int day)
    {
        return System.IO.File.ReadAllText($"Data/Day{day}.txt");
    }
}