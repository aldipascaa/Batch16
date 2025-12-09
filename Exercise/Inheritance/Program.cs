namespace Inheritance;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            //1. Basic Inheritance - Foundation Concept
            Stage("BASIC INHERITANCE");
            BasicInheritanceDemo.RunDemo();

            //2. Polymorphism - Many forms, one interface
            Stage("POLYMORPHISM");
            PolymorphismDemoClass.RunDemo();

            //3. Casting and Reference Converstions
            Stage("CASTING AND CONVERSTIONS");
            CastingAndConversionsDemo.RunDemo();

            //4. Virtual Methods and Overriding
            Stage("VIRTUAL METHODS AND OVERRIDING");
            VirtualOverrideDemo.RunDemo();

            //5. Member Hiding with 'new' keyword
            Stage("MEMBER HIDING");
            MemberHidingDemo.RunDemo();

            //6. Base keyword usage
            Stage("BASED KEYWORD");
            BaseKeywordDemo.RunDemo();

            //7. Constructor Inheritance and Required Members
            Stage("CONSTRUCTOR INHERITANCE");
            ConstructorInheritanceDemo.RunDemo();

            //8. Sealed Classes and Methods
            Stage("SEALED CONCEPTS");
            SealedDemo.RunDemo();

            //9. Overload Resolution with Inheritance
            Stage("OVERLOAD RESOLUTION");
            OverloadResolutionDemo.RunDemo();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during demonstration: {ex.Message}");
            Console.WriteLine("This might be due to missing dependencies or runtime issues.");
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
    static void Stage(string x)
    {
        Console.Write($"Press any key to start with {x}");
        Console.ReadLine();
        Console.Clear();
    }
}