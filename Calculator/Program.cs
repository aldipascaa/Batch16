using MyCalculator;

class Program
{
    public static void Main(string[] args)
    {
        Calculator calculator = new Calculator();  
        int result = calculator.Add(3,4);
        Console.WriteLine(result);
    }
}