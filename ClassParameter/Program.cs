class Cat
{
    public void Eat(string? makanan)
    {
        Console.WriteLine("Kucing memakan " + makanan);
    }

    public void Poop()
    {
        Console.WriteLine("Kucing poop!!!");
    }
}
class Program
{
    static void Main()
    {
        Cat cat = new Cat();
        Console.WriteLine("Sebutkan makanan kucing kamu!");
        string? makan = Console.ReadLine();
        cat.Eat(makan);
        Console.WriteLine("...");
        Thread.Sleep(1000);
        cat.Poop();
    }
}