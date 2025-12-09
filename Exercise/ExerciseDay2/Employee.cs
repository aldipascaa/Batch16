#Demonstrates a basic class structure
using System;
namespace classes
{
    public class Employee
    {
        //Prifate fields = the internal data of our project
        //using -camelCase naming convention for private fields
        private string _name;
        private int _age;
        private readonly DateTime _hireDate;
        private static int _totalEmployees = 0;
        private static int _totalWorkHours = 0;

        //Static properties to expose the counters
        public static int TotalEmployees => _totalEmployees;
        public static int TotalWorkHours => _totalWorkHours;

        public Employee(string name, int age)
        {
            this._name = name;
            this._age = age;
            this._hireDate = DateTime.Now;

            _totalEmployees++;

            Console.WriteLine($" New Employee hired: {name} (Total employees: {_totalemployees})");
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                _age = value; 
            }
        }
        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 0 || value > 150)
                {
                    throw new ArgumentException("Age must be between 0 and 150");
                }
                _age = value;
            }
        }
        // Read-Only property - no setter, exposes readonly field
        public DateTime HireDate => _hireDate;

        public int YearsOfService => DateTime.Now.Year - _hireDate.Year;

        ///Summary
        /// instance method - behavior that this employee can perform
        /// shows how methods can modify onject state
        
        public void CelebrateBirthday()
        {
            _age++;
            Console.WriteLine($"Happy birthday {_name}! Now {_age} years old");
        }

        //Another instance method showing how employees work
        public void Work()
        {
            int hoursWorked = 8;
            _totalWorkHours += hoursWorked;
            Console.WriteLine($"{_name} worked {hoursWorked} hours today");
        }

        //static method - belongs to the employee class, not any specific instance

        public static void GetCompanyStats()
        {
            Console.WriteLine($" Company Stats:");
            Console.WriteLine($"    Total Employees: {_totalEmployees}");
            Console.WriteLine($"    Total Work Hours: {_totalWorkHours}");
            Console.WriteLine($"    Average Hours per Employee: {(_totalEmployees > 0? _totalWorkHours / _totalEmployees : 0)}")
        }

        //Overiride toString to provide meaningful string representation
        public override string ToString()
        {
            return $"{_name} (age:{_age}, Hired: {_hireDate:yyyy-MM-dd}, Service: {YearsOfService} years)";
        }
        
        //Finalizer - called by garbage collector when the object being destroyed
        ~Employee()
        {
            _totalEmployees--;
            Console.WriteLine("");
        }
    }
}