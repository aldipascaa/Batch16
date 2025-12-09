namespace Classes;

public class Employee
{
    private string _name;
    private int _age;
    readonly DateTime _hireDate;
    private static int _totalEmployees = 0;
    private static int _totalWorkHours = 0;

    public static int TotalEmployees => _totalEmployees;
    public static int TotalWorkHours => _totalWorkHours;

    public Employee(string name, int age)
    {
        while (string.IsNullOrWhiteSpace(name)||name.Length < 3)
        {
            Console.WriteLine("Your Employee name cannot less than 3 word");
            Console.WriteLine("Rename your new Employee:");
            name = Console.ReadLine()??" ";
        }
        
        this._name = name;
        this._age = age;
        this._hireDate = DateTime.Now;
        _totalEmployees++;

        Console.WriteLine($"New empoyee hired: {name} (Total employees: {_totalEmployees})");
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int age
    {
        get { return _age; }
        set { _age = value; }
    }

    public DateTime HireDate => _hireDate;

}