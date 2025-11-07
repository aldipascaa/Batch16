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
        var empoyee = new Employee("al", 25);
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
    { }

    static void DemonstrateProperties()
    { }

    static void DemonstrateIndexers()
    { }

    static void DemonstrateObjectFeatures()
    { }

    static void DemonstrateStaticFeatures()
    { }

    static void DemonstratePrimaryConstructors()
    { }

    static void DemonstratePartialClasses()
    { }

    static void DemonstrateThisKeyword()
    { }

    


}


