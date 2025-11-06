class Person(string firstName, string lastName)
{
    public readonly string FirstName = firstName;
    public string LastName {get;} = lastName;
    public void Print() => Console.WriteLine("Hi " + FirstName + " " + LastName);
}

public class MathUtils
{
    public static double Square(double x)
    {
        return x*x;
    }
}

Console.WriteLine("What your First Name");
string input1 = Console.ReadLine();

Console.WriteLine("What your Last Name");
string input2 = Console.ReadLine();

Person p = new Person(input1, input2);

p.Print();
Console.WriteLine(MathUtils.Square(5));