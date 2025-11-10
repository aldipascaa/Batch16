namespace Classes;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("C# Classes");

        DemonstrateBasicClasses();

        DemonstrateFields();

        DemonstrateMethods();

        DemonstrateConstructors();

        DemonstrateProperties();

        DemonstrateIndexers();

        DemonstrateObjectFeatures();

        DemonstrateStaticFeatures();

        DemonstratePrimaryConstructors();

        DemonstratePartialClasses();

        DemonstrateThisKeyword();


    }
    static void DemonstrateBasicClasses()
    {
        Console.WriteLine("1. Basic Classes");
        //Create employee instances from Employee class blueprint
        var empoyee = new Employee("Aldi", 25);
        var manager = new Employee("Bob", 30);

        Console.WriteLine($" Employee: {empoyee.Name}");
        Console.WriteLine($" Manager: {manager.Name}");
    }

    static void DemonstrateFields()
    {
        Console.WriteLine("2. Fields");

        //Instance fields - each object gets its own copy
        var octopus = new Octopus("Oscar");
        var octopus2 = new Octopus("Coby");
        Console.WriteLine($" Octopus 1: {octopus.Name}, Age: {octopus.Age}");
        Console.WriteLine($" Octopus 2: {octopus2.Name}, Age: {octopus2.Age}");
        // Static fields - shared across ALL instances
        Console.WriteLine($"  All octopuses have {Octopus.Legs} legs (static field)");
        Console.WriteLine($"  Total octopuses created: {Octopus.TotalCreated}");
        // Readonly fields
        Console.WriteLine($"Octopus 1 ID (readonly): {octopus.Id}");
        Console.WriteLine($"Octopus 2 ID (readonly): {octopus2.Id}");

        octopus.Hide();
        octopus2.Feed();
        octopus2.Sleep();
        octopus.ToString();
           
    }

    static void DemonstrateConstants()
    {
        Console.WriteLine("3. Constants vs static Readonly:");

        Console.WriteLine($" PI (const): {MathConstants.PI}");
        Console.WriteLine($" Speed of light (const): {MathConstants.SPEED_OF_LIGHT}");
        Console.WriteLine($" App Start Time (Static readonly): {MathConstants.ApplicationStartTime}");
        Console.WriteLine($" Random Seed (static readonly): {MathConstants.RandomSeed}");

        //local constants
        const int LOCAL_MAX = 100;
        Console.WriteLine(LOCAL_MAX);

    }

    static void DemonstrateMethods()
    {
        Console.WriteLine($"4. Methods - Object Behaviors: ");
        var math = new MathOperations();
        Console.WriteLine($" Substraction: 15 + 25 = {math.Add(15 + 25)}");
        Console.WriteLine($" Factorial of 5: {math.CalculateFactorial(5)}");

        Console.WriteLine($" Power (int): 2^8 = {math.Power(2, 8)}");
        Console.WriteLine($" Power (Double): 2.5^3 = {math.Power(2.5,3)}");
    }

    static void DemonstrateConstructors()
    {
        Console.WriteLine("5. Constructors");

        var Car1 = new Car("Toyota");
        var Car2 = new Car("Honda", "Civic");
        var Car3 = new Car("BMW", "X5", 2023);

        Console.WriteLine($" Car 1: {Car1.Make} {Car1.Model} {Car1.Year}");
        Console.WriteLine($" Car 2: {Car2.Make} {Car2.Model} {Car2.Year}");
        Console.WriteLine($" Car 3: {Car3.Make} {Car3.Model} {Car3.Year}");

    }

    static void DemonstrateProperties()
    {
        Console.WriteLine("6. Properties - Controlled Data Access:");

        var stock = new Stock();

        stock.Symbol = "MSFT";
        stock.CompanyName = "Microsoft Corporation";
        Console.WriteLine($" Stock: {stock.Symbol} - {stock.CompanyName}");

        stock.CurrentPrice = 350.75m;
        Console.WriteLine($" Current price: ${stock.CurrentPrice:F2}");

        try
        {
            stock.CurrentPrice = -10;
        }
        catch(ArgumentException ex)
        {
            Console.WriteLine($" x {ex.Message}");
        }
    }

    static void DemonstrateIndexers()
    {
        Console.WriteLine("7. Indexers");
    }

    static void DemonstrateObjectFeatures()
    {
        Console.WriteLine("8. Object Features");

        var bunny = new Bunny
        {
            Name = "Hoppy",
            LikesCarrots = true,
            LikesHumans = true,
            Age = 4
        };

        bunny.Feed("carrots");
        bunny.Hop();
        bunny.Pet();
        Console.WriteLine(bunny);
    }

    static void DemonstrateStaticFeatures()
    { }

    static void DemonstratePrimaryConstructors()
    { }

    static void DemonstratePartialClasses()
    { }

    static void DemonstrateThisKeyword()
    { }

    


}


