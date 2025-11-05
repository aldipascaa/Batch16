namespace MyFirstTry
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose a number for x:");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Your number is :"+ x);

            for (int i = 1; i<= x ; i++)
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
    }
}