class Home
{
    public int Door;
    public int Window;
    public string? Color;
    public string? Address;

    public Home(int doors, int windows, string color, string address)
    {
        Door = doors;
        Window = windows;
        Color = color;
        Address = address;

        Console.WriteLine($"Home Created: ");
        Console.WriteLine($"    Doors: {Door}");
        Console.WriteLine($"    Windows: {Window}");
        Console.WriteLine($"    Color: {Color}");
        Console.WriteLine($"    Address: {Address}");
    }
    public Home(int door, int window)
    : this(door, window, "Unknown", "No address"){}
    public Home()
    : this(1, 2, "White", "No adress"){}
}

class Program
{
    static void Main()
    {
        //full information
        Home home1 = new Home(2,4,"black","salatiga");

        //partial information
        Home home2 = new Home(3,5);

        //no information(default)
        Home home3 = new Home();
    }
}