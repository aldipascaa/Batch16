using System.ComponentModel;
using System.Linq.Expressions;
namespace LogicExercise;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Pick a number!!");
        int x = Convert.ToInt32(Console.ReadLine());
        //FirstLogic(x);
        //Console.WriteLine();
        //SecondLogic(x);
        //Console.WriteLine();
        ThirdLogic(x);
        Console.WriteLine();
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
    // Refactored FourthLogic using LINQ
    static void FourthLogic(int x)
    {
        var dictionary = new Dictionary<int, string>()
        {
            {3, "foo"},
            {4, "baz"},
            {5, "bar"},
            {7, "jazz"},
            {9, "huzz"}
        };
        for (int i = 1; i <= x; i++)
        {
            string output = string.Concat(dictionary.Where(kv => i % kv.Key == 0).Select(kv => kv.Value));
            if (i != x) output += output != "" ? "," : $"{i},";
            Console.Write(output);
        };
    }
} 

