using System.ComponentModel;
using System.Linq.Expressions;

namespace LogicExercise;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Pick a number!!");
        int x = Convert.ToInt32(Console.ReadLine());
        Looping(x);
    }

    static void Looping(int x)
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
}