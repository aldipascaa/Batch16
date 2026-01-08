using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace LogicExercise;

class Program
{
    static void Main()
    {
        Console.WriteLine("Pick a number!!");
        int x = Convert.ToInt32(Console.ReadLine());
        MyClass myClass = new MyClass();
        myClass.AddRule(3, "foo");
        myClass.AddRule(4, "baz");
        myClass.AddRule(5, "bar");
        myClass.AddRule(7, "jazz");
        myClass.AddRule(9, "huzz");
        myClass.PrintRange(x);
    }
    static void FirstLogic(int x)
    {
        for (int i = 1; i <= x;i++)
        {
            if (i % 3 == 0 & i % 5 == 0) Console.Write("foobar");
            else if (i % 3 == 0) Console.Write("foo");
            else if (i % 5 == 0) Console.Write("bar");
            else Console.Write(i);
            if (i != x) Console.Write(", ");
        }
    }
    // Refactored SecondLogic using boolean variables
    static void SecondLogic(int x)
    {
        for (int i = 1; i <= x; i++)
        {
            bool logic1 = i % 3 == 0;
            bool logic2 = i % 5 == 0;
            bool logic3 = i % 7 == 0;
            Console.Write((logic1 ? "foo" : "") + (logic2 ? "bar" : "") + (logic3 ? "jazz" : ""));
            if (i!=x)
                Console.Write(logic1 || logic2 || logic3?", ": $"{i}, ");
        }
    }
    // Refactored ThirdLogic using Dictionary
    static void ThirdLogic(int x)
    {
        // Using Dictionary to store the key value pairs
        var dictionary = new Dictionary<int, string>()
        {
            {3, "foo"},
            {4, "baz"},
            {5, "bar"},
            {7, "jazz"},
            {9, "huzz"}
        };
        // Loop through numbers from 1 to x
        for (int i = 1; i <= x; i++)
        {
            var output = new StringBuilder();
            var outputLengthBefore = output.Length;
            foreach (var (key,value) in dictionary) output.Append(i % key == 0 ? value : "");
            if (outputLengthBefore == output.Length) output.Append(i);
            if (i != x) output.Append(output.Length > 0 ? ", " : $"{i}, ");
            Console.Write(output);
        };
    }
    // Refactored FourthLogic that holds logic rules and allows adding new ones at runtime.
    class MyClass
    {
        private readonly List<(int Key, string Value)> _rules = new();
        // Preferred C# naming
        public void AddRule(int key, string value)=> _rules.Add((key, value));
        public string GetOutputFor(int i)
        {
            var sb = new StringBuilder();
            foreach (var (key,value) in _rules) if (key != 0 && i % key == 0) sb.Append(value);
            return sb.Length > 0 ? sb.ToString() : i.ToString();
        }
        // Prints 1..x using the rules, matching the comma-separated format used elsewhere
        public void PrintRange(int x)
        {
            for (int i = 1; i <= x; i++)
            {
                Console.Write(GetOutputFor(i));
                if (i != x) Console.Write(", ");
            }
        }
    }
} 

