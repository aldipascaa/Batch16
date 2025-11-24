using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
namespace LogicExercise;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Pick a number!!");
        int x = Convert.ToInt32(Console.ReadLine());
        ThirdLogic(x);
        Console.WriteLine("");
        // Demonstration using MyClass to add logic at runtime
        var my = new MyClass();
        my.AddLogic("foo", 3);
        my.AddLogic("baz", 4);
        my.AddLogic("bar", 5);
        my.AddLogic("jazz", 7);
        my.AddLogic("huzz", 9);

        my.PrintRange(x);
    }
    // First Logic Implementation using if-else statements
    static void FirstLogic(int x)
    {
        for (int i = 1; i <= x;i++)
        {
            if (i % 3 == 0 & i % 5 == 0){
                Console.Write("foobar");
            }
            else if (i % 3 == 0){
                Console.Write("foo");
            }
            else if (i % 5 == 0){
                Console.Write("bar");
            }
            else Console.Write(i);
            if (i != x)
            {
            Console.Write(",");
            }
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
                Console.Write(logic1 || logic2 || logic3?",": $"{i},");
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
            string output = "";
            foreach (var (key,value) in dictionary) output += i % key == 0 ? value : "";
            if (i != x) output += output != "" ? "," : $"{i},";
            Console.Write(output);
        };
    }
    // Refactored FourthLogic that holds logic rules and allows adding new ones at runtime.
    class MyClass
    {
        private readonly List<KeyValuePair<int, string>> _rules = new();
        // Preferred C# naming
        public void AddLogic(string value, int key)
        {
            _rules.Add(new KeyValuePair<int, string>(key, value));
        }
        // Returns either the concatenated values or the number as string
        
        public string GetOutputFor(int i)
        {
            var sb = new StringBuilder();
            foreach (var kv in _rules)
            {
                if (kv.Key != 0 && i % kv.Key == 0)
                    sb.Append(kv.Value);
            }
            return sb.Length > 0 ? sb.ToString() : i.ToString();
        }
        // Prints 1..x using the rules, matching the comma-separated format used elsewhere
        public void PrintRange(int x)
        {
            for (int i = 1; i <= x; i++)
            {
                Console.Write(GetOutputFor(i));
                if (i != x) Console.Write(",");
            }
        }
    }
} 

