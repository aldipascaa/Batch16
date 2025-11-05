// See https://aka.ms/new-console-template for more information
Console.WriteLine("Number for x!");
int x = int.Parse(Console.ReadLine());
Console.WriteLine("Your Number is :" + x);
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