namespace calculators;

public class Calculator
{
    // Instance field - each Calculator object has its own
    private double _lastResult;
    
    // Static field - shared by all Calculator instances
    private static int _calculationCount;
    
    // Instance property
    public double LastResult => _lastResult;
    
    // Static property
    public static int TotalCalculations => _calculationCount;
    
    // Instance method - operates on specific object
    public double Add(double a, double b)
    {
        _lastResult = a + b;
        _calculationCount++; // Can access static members
        return _lastResult;
    }
    
    // Static method - operates on class level
    public static double Multiply(double a, double b)
    {
        _calculationCount++; // Can only access static members
        return a * b;
    }
    
    // Static constructor - runs once when class is first used
    static Calculator()
    {
        _calculationCount = 0;
        Console.WriteLine("Calculator class initialized");
    }
}

// Usage demonstration
Calculator calc1 = new Calculator();
Calculator calc2 = new Calculator();

double result1 = calc1.Add(5, 3);        // Instance method call
double result2 = Calculator.Multiply(4, 2); // Static method call

Console.WriteLine($"Calc1 last result: {calc1.LastResult}"); // 8
Console.WriteLine($"Calc2 last result: {calc2.LastResult}"); // 0
Console.WriteLine($"Total calculations: {Calculator.TotalCalculations}"); // 2