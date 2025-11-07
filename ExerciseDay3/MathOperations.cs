using System.Globalization;

namespace Classes;

public class MathOperations
{
    public int Add(int a, int b) => a + b;
    public int Subtract(int a, int b) => a - b;
    public int Multiply(int a, int b) => a * b;
    public int Divide(int a, int b) => a / b;
    public bool IsEven(int number) => number % 2 == 0;
    public double Divide(double a, double b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero!");
        }

        double result = a / b;
        Console.WriteLine($"Dividing {a} by {b} = {result}");
        return result;
    }
    
    public long CalculateFactorial(int n)
    {
        if (n < 0)
            throw new ArgumentException("");
        long FactorialHelper(int number)
        {
            if (number <= 1)
                return 1;
            return number * FactorialHelper(number - 1);
        }
        long result = FactorialHelper(n);
        return result;
    }
    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }
    public double Power(double @base, int exponent = 2)
    {
        return Math.Pow(@base, exponent);
    }

    public int Add(params int[] numbers)
    {
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }
        return sum;
    }

    public void CalcolateSumAndProduct(int a, int b, out int sum, out int product)
    {
        sum = a + b;
        product = a * b;
        Console.WriteLine($"Calculated sum: {sum}, product: {product}");
    }

    public void SquarebyReferense(ref int value)
    {
        Console.WriteLine($"Original value: {value}");
        value = value * value;
        Console.WriteLine($"Square value: {value}");
    }

    public override string ToString()
    {
        return "Math Operations Calculation - Lets go!!!";
    }
}