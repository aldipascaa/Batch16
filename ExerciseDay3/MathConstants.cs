namespace Classes;

public static class MathConstants
{
    public const double PI = 3.14159265358979323846;
    public const double E = 2.71828182845904523536;
    public const double SPEED_OF_LIGHT = 299792458;
    public const int AVOGADRO_NUMBER_POWER = 23;
    public const string MATH_LIBRARY_VERSION = "1.0.0";
    public const string AUTHOR = "C# Training Team";
    public static readonly DateTime ApplicationStartTime = DateTime.Now;
    public static readonly int RandomSeed = new Random().Next(1000, 9999);
    public static readonly string ComputerName = Environment.MachineName;
    public static readonly string CurrentUser = Environment.UserName;
    public const int SECOND_PER_MINUTE = 60, MINUTES_PER_HOUR = 60, HOURS_PER_DAY = 24;

}