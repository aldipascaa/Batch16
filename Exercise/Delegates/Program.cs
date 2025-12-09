using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

namespace DelegatesDemo;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Delegates in C# - Complete Demonstration");

        //Run Demonstration
        BasicDelegateDemo();
        PluginMethodsDemo();
        InstanceAndStaticMethodTargetsDemo();
        MulticastDelegatesDemo();
        GenericDelegatesDemo();
        FuncAndActionDelegatesDemo();
        DelegateVsInterfaceDemo();
        DelegateCompatibilityDemo();
        ParameterCompatibilityDemo();
        ReturnTypeCompatibilityDemo();
        RealworldScenarioDemo();
        int max = int.MaxValue;
        int result = max + 1;
        int x = unchecked(max+1);
        Console.WriteLine(result);
        Console.WriteLine(x);
        int? y = null; int? w = y?.ToString()?.Length;
        Console.WriteLine(y);
        Console.WriteLine(w);

    }

    delegate int Transformer(int x);

    static void BasicDelegateDemo()
    {
        Transformer t = Square;

        int result = t(3);
        Console.WriteLine(result);

        t = Cube;
        result = t(3);
        Console.WriteLine(result);

        result = t.Invoke(3);
        Console.WriteLine(result);
    }

    static int Square(int x) => x * x;
    static int Cube(int x) => x * x * x;
    static void PluginMethodsDemo()
    {
        int[] values = { 1, 2, 3, 4, 5 };
        Console.WriteLine($"Original values: [{string.Join(", ", values)}]");

        Transform(values, Square);
        Console.WriteLine($"After Square transform:[{string.Join(",", values)}]");
    }

    static void Transform(int[] values, Transformer t)
    {
        for (int i =0; i < values.Length; i++)
        {
            values[i] = t(values[i]);
        }
    }
    static void InstanceAndStaticMethodTargetsDemo()
    {
        Console.WriteLine("Static Method delegation:");
        Transformer staticDelegate = Square;
        Console.WriteLine("Static Square of 6: " + staticDelegate.Invoke(6));

        Console.WriteLine("Instance method delegation:");
        Calculator calc = new Calculator(5);
        Transformer InstanceDelegate = calc.MultiplyBy;
        Console.WriteLine(new Calculator(8).MultiplyBy(8));

        Console.WriteLine($"Multiply 8 by {calc.Multiplier}: {InstanceDelegate(8)}");

        Calculator calc2 = new Calculator(3);
        Transformer instanceDelegate2 = calc2.MultiplyBy;
        Console.WriteLine($"Difference instance - 8 * {calc2.Multiplier}: {instanceDelegate2(8)}");
        Console.WriteLine("");
    }

    public class Calculator
    {
        private int multiplier;

        public Calculator(int multiplier)
        {
            this.multiplier = multiplier;
        }

        public int Multiplier => multiplier;

        public int MultiplyBy(int input)
        {
            return input * multiplier;
        }
    }

    delegate void ProgressReporter(int percentComplete);
    static void MulticastDelegatesDemo()
    {
        Console.WriteLine("================================");
        ProgressReporter? reporter = WriteProgressToConsole;

        reporter += WriteProgressToFile;
        reporter += SendProgressAlert;

        Console.WriteLine("Progress reporting with multicast delegate (3 methods):");
        reporter(50);

        Console.WriteLine("Removing console reporter using -= operator:");
        reporter -= WriteProgressToConsole;

        Console.WriteLine("Progress reporting after removal (2 methods):");
        if (reporter != null)
        {
            reporter(75);
        }
        // Demonstrate that return values are lost in multicast (except the last one)
        Console.WriteLine("\nMulticast with return values (only last one is kept):");
        Transformer multiTransformer = Square;
        multiTransformer += Cube;  // Now has two methods
        
        int lastResult = multiTransformer(3);  // Calls Square(3) then Cube(3)
        Console.WriteLine($"Only the last result is returned: {lastResult}");  // Will be 27 (cube), not 9 (square)
        
        Console.WriteLine();
    }

    static void WriteProgressToConsole(int percentComplete)
    {
        Console.WriteLine($"Console Log: {percentComplete}% complete");
    }
    static void WriteProgressToFile(int percentComplete)
    {
        Console.WriteLine($" File Log: Writing {percentComplete}% to progress.log)");
    }
    static void SendProgressAlert(int percentComplete)
    {
        if (percentComplete >= 75)
        {
            Console.WriteLine($"Alert: High progress reached - {percentComplete}");
        }
    }
    public delegate TResult Transformer<TArg, TResult>(TArg arg);
    static void GenericDelegatesDemo()
    {
        // Same delegate type, different type arguments
        Transformer<int, int> intSquarer = x => x * x;
        Transformer<string, int> stringLength = s => s.Length;
        Transformer<double, string> doubleFormatter = d => $"Value: {d:F2}";
            
        Console.WriteLine($"Int squarer (5): {intSquarer(5)}");
        Console.WriteLine($"String length ('Hello'): {stringLength("Hello")}");
        Console.WriteLine($"Double formatter (3.14159): {doubleFormatter(3.14159)}");

        // Using generic Transform method
        Console.WriteLine("\nGeneric Transform method demo:");
        int[] numbers = { 1, 2, 3, 4 };
        Console.WriteLine($"Original numbers: [{string.Join(", ", numbers)}]");
        
        TransformGeneric(numbers, x => x * x);  // Square each number
        Console.WriteLine($"Squared numbers: [{string.Join(", ", numbers)}]");
        
        string[] words = { "cat", "dog", "elephant" };
        Console.WriteLine($"Original words: [{string.Join(", ", words)}]");
        
        TransformGeneric(words, s => s.ToUpper());  // Uppercase each word
        Console.WriteLine($"Uppercase words: [{string.Join(", ", words)}]");
        
        Console.WriteLine();
        
    }
    
    public static void TransformGeneric<T>(T[] values, Transformer<T,T> transformer)
    {
        for(int i = 0; i < values.Length; i++)
        {
            values[i] = transformer(values[i]);
        }
    }
    static void FuncAndActionDelegatesDemo()
    {
        Console.WriteLine("6. FUNC AND ACTION DELEGATES - BUILT-IN CONVENIENCE");
        Console.WriteLine("===================================================");

        // Func delegates return values
        // Func<TResult> - no parameters, returns TResult
        // Func<T, TResult> - one parameter of type T, returns TResult
        // ... up to Func<T1, T2, ..., T16, TResult>
        
        Func<int, int> squareFunc = x => x * x;
        Func<int, int, int> addFunc = (a, b) => a + b;
        Func<string> getTimeFunc = () => DateTime.Now.ToString("HH:mm:ss");
        
        Console.WriteLine($"Func square of 7: {squareFunc(7)}");
        Console.WriteLine($"Func add 5 + 8: {addFunc(5, 8)}");
        Console.WriteLine($"Func current time: {getTimeFunc()}");
        
        // Action delegates return void
        // Action - no parameters
        // Action<T> - one parameter of type T
        // ... up to Action<T1, T2, ..., T16>
        
        Action simpleAction = () => Console.WriteLine("  Simple action executed");
        Action<string> messageAction = msg => Console.WriteLine($"  Message: {msg}");
        Action<int, string> complexAction = (num, text) => 
            Console.WriteLine($"  Number: {num}, Text: {text}");
        
        Console.WriteLine("Action demonstrations:");
        simpleAction();
        messageAction("Hello from Action!");
        complexAction(42, "The Answer");
        
        // The beauty: our Transform method can now use Func instead of custom delegate
        Console.WriteLine("\nUsing Func with Transform method:");
        int[] values = { 1, 2, 3, 4, 5 };
        TransformWithFunc(values, x => x * 2);  // Double each value
        Console.WriteLine($"Doubled values: [{string.Join(", ", values)}]");
        
        Console.WriteLine();
    }

    public static void TransformWithFunc<T>(T[] values, Func<T, T> transformer)
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = transformer(values[i]);
        }
    }
    static void DelegateVsInterfaceDemo()
    {
        Console.WriteLine("7. DELEGATES VS INTERFACES - WHEN TO USE WHAT");
        Console.WriteLine("=============================================");

        // Interface approach
        Console.WriteLine("Interface approach:");
        ITransformer squareTransformer = new SquareTransformer();
        ITransformer cubeTransformer = new CubeTransformer();

        TransformWithInterface(new int[] { 2, 3, 4 }, squareTransformer);
        TransformWithInterface(new int[] { 2, 3, 4 }, cubeTransformer);

        // Delegate approach (more concise for single-method scenarios)
        Console.WriteLine("\nDelegate approach:");
        Func<int, int> squareDelegate = x => x * x;
        Func<int, int> cubeDelegate = x => x * x * x;

        TransformWithDelegate(new int[] { 2, 3, 4 }, squareDelegate);
        TransformWithDelegate(new int[] { 2, 3, 4 }, cubeDelegate);

        // Multiple implementations from single class (advantage of delegates)
        Console.WriteLine("\nMultiple implementations from single class:");
        MathOperations math = new MathOperations();

        // One class, multiple compatible methods
        TransformWithDelegate(new int[] { 2, 3, 4 }, math.Square);
        TransformWithDelegate(new int[] { 2, 3, 4 }, math.Cube);
        TransformWithDelegate(new int[] { 2, 3, 4 }, math.Double);

        Console.WriteLine();
    }

    interface ITransformer
    {
        int Transform(int x);
    }
    class SquareTransformer : ITransformer
    {
        public int Transform(int x) => x * x;
    }
    class CubeTransformer : ITransformer
    {
        public int Transform(int x) => x * x * x;
    }

    static void TransformWithInterface(int[] values, ITransformer transformer)
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = transformer.Transform(values[i]);
        }
        Console.WriteLine($" Result: [{string.Join(", ", values)}]");
    }

    class MathOperations
    {
        public int Square(int x) => x * x;
        public int Cube(int x) => x * x * x;
        public int Double(int x) => x * x;
    }

    static void TransformWithDelegate(int[] values, Func<int, int> transformer)
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = transformer(values[i]);
        }
        Console.WriteLine($"Result: [{string.Join(", ", values)}]");
    }

    delegate void D1();
    delegate void D2();
    static void DelegateCompatibilityDemo()
    {
        void TestMethod() => Console.WriteLine("Test method executed");

        D1 d1 = TestMethod;

        D2 d2 = new D2(d1);

        Console.WriteLine("Both call the same method");
        d1();
        d2();

        // Delegate equality based on method targets and order
        D1 d1Copy = TestMethod;
        Console.WriteLine($"d1 == d1Copy (same method): {d1 == d1Copy}");  // True
        
        // Multicast delegates - equality depends on all methods in same order
        D1 d1Multi = TestMethod;
        d1Multi += TestMethod;  // Now has two references to TestMethod
        
        Console.WriteLine($"d1 == d1Multi (different invocation lists): {d1 == d1Multi}");  // False
        
        d1Multi();
    }
    static void ParameterCompatibilityDemo() { }
    static void ReturnTypeCompatibilityDemo() { }
    static void RealworldScenarioDemo() { }

}