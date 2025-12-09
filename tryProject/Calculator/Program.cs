using MyCalculator;

class Program
{
    static void Main()
    {
        Calculator calculator = new Calculator();  
        //int result = calculator.Add(3,4);
        //Console.WriteLine(result);

        

        Console.WriteLine("Input first number:");
        int inputUser = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Input second number:");
        int inputUser2 = Convert.ToInt32(Console.ReadLine());

        int result = 0;

        Console.WriteLine("choose your operator:");
        Console.WriteLine("1. Add");
        Console.WriteLine("2. Substract");
        Console.WriteLine("3. Multiply");
        Console.WriteLine("4. Divide");

        string? operatorUser = Console.ReadLine();
        if (operatorUser == "1")
        {
            result = calculator.Add(inputUser,inputUser2);

        }
        else if (operatorUser == "2")
        {
            result = calculator.Subtract(inputUser,inputUser2);
        }
        else if (operatorUser == "3")
        {
            result = calculator.Multiply(inputUser,inputUser2);
        }
        else
        {
            result = calculator.Divide(inputUser,inputUser2);
        }
        Console.WriteLine(result);

    }
}