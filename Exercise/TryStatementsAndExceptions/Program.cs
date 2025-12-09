namespace ExceptionHandlingDemo;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Exception Handling in C# - Complete Training Demonstration ===\n");
        Console.WriteLine("This program demonstrates all major concepts of exception handling:");
        Console.WriteLine("• Basic try-catch blocks");
        Console.WriteLine("• Multiple catch clauses with specific exception types");
        Console.WriteLine("• Exception filters with 'when' keyword");
        Console.WriteLine("• Finally blocks for cleanup");
        Console.WriteLine("• Using statements for automatic resource disposal");
        Console.WriteLine("• Throwing and rethrowing exceptions");
        Console.WriteLine("• TryXXX pattern as alternative to exceptions");
        Console.WriteLine("• Real-world exception handling scenarios\n");

        // Run all demonstrations in order
        BasicTryCatchDemo();
        MultipleCatchBlocksDemo();
        ExceptionFiltersDemo();
        FinallyBlockDemo();
        UsingStatementDemo();
        UsingDeclarationDemo(); // New C# 8+ feature
        ThrowingExceptionsDemo();
        ThrowExpressionsDemo(); // C# 7+ feature
        RethrowingExceptionsDemo();
        CommonExceptionTypesDemo(); // New section
        TryParsePatternDemo();
        ArgumentNullThrowIfNullDemo(); // .NET 6+ feature
        ReturnCodesAlternativeDemo();
        RealWorldScenarioDemo();

        Console.WriteLine("=== Training Summary ===");
        Console.WriteLine("You've now seen comprehensive examples of exception handling in C#.");
        Console.WriteLine("Remember: Use exceptions for truly exceptional cases, not for normal program flow!");
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
    static void BasicTryCatchDemo()
    {

        // This demonstrates the basic structure: try { risky code } catch { handle error }
        Console.WriteLine("Testing division by zero - without try-catch this would crash:");

        try
        {
            // This line will throw a DivideByZeroException
            int result = Calc(0);
            Console.WriteLine($"Result: {result}"); // This line won't execute
        }
        catch (DivideByZeroException ex)
        {
            // Execution jumps here when the exception is thrown
            Console.WriteLine("✓ Caught DivideByZeroException - program continues running");
            Console.WriteLine($"  Exception message: {ex.Message}");
            Console.WriteLine($"  Exception type: {ex.GetType().Name}");
        }

        Console.WriteLine("✓ Program execution continues after exception handling\n");

        // Important principle: Prevention is better than exception handling
        Console.WriteLine("Better approach - validate input before risky operations:");
        int safeResult = SafeCalc(0);
        Console.WriteLine($"Safe result: {safeResult}");
        Console.WriteLine("Remember: Exceptions are expensive - use them for truly exceptional situations!\n");
    }
    static int Calc(int x)
    {
        return 10 / x;
    }
    static int SafeCalc(int x)
    {
        if (x == 0)
        {
            Console.WriteLine("Warning: Division by zero attempted, returning safe value");
            return 0;
        }
        return 10 / x;
    }
    static void MultipleCatchBlocksDemo() { }
    static void TestParsingScenarios()
    {
        string[] testCases = { "100", "abc", "500", "" };
        foreach(string testCase in testCases)
        {
            Console.WriteLine($"Testing '{testCase}':");
            {
                try
                {
                    byte result = byte.Parse(testCase);
                    Console.WriteLine("Success");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid format");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Number are too large for byte");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine(" Empty string");
                }
            }
        }
    }
    static void ExceptionFiltersDemo() { }
    static void FinallyBlockDemo() { }
    static void UsingStatementDemo() { }
    static void UsingDeclarationDemo() { }
    static void ThrowingExceptionsDemo() { }
    static void ThrowExpressionsDemo() { }
    static void RethrowingExceptionsDemo() { }
    static void CommonExceptionTypesDemo() { }
    static void TryParsePatternDemo() { }
    static void ArgumentNullThrowIfNullDemo() { }
    static void ReturnCodesAlternativeDemo() { }
    static void RealWorldScenarioDemo() { }
}