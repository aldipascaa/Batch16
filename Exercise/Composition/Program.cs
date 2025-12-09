using Automotive;
using CarComponents;

class Program
{
    static void Main()
    {
        Engine engine = new Engine("V8", "5.0", "Mercedess");
        Tire tire = new Tire(18, "Rubber", "Michelin");
        Car car = new Car(engine, tire);

        Console.WriteLine("Available Car:");
        Console.WriteLine("Type : " + engine.engineType);
        Console.WriteLine("Brand : " + engine.engineBrand);
    }
}