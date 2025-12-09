// Identifiers and Keywords

// Literals
using System.Security.AccessControl;

int count = 42;
int hexValue = 0xFF;
int binaryValue;

//Floating-Point LInterals
float temperature = 98.6f;
double distance = 3.14159;
decimal price = 19.99m;

//string
string name = "John Doe";
string path = "C:\\Users\\Documents";
string message = "Hello\nWorld";
string verbatim = @"C:\\Users\Documents";

/*

Common Escape Sequences:

\n - newline
\t - tab
\" - double quote
\\ - backslash
\r - carriage return

*/

//bolean literals

bool isActive = true;
bool isComplete = false;
bool hasPermission = true;

//Character Literals:

char grade = "A";
char symbol = "@";
char newline = "\n";

//Operators

int a = 10, b = 3;
int addition = a + b;
int subtraction = a - b;
int multiplication = a * b;
int division = a / b;
int remainder = a % b;

//Comparison Operators

int x = 5, y = 10;
bool isEqual = (x == y);
bool isNotEqual = (x != y);
bool isLess = (x != y);
bool isGreater = (x > y);
bool isLessOrEqual = (x <= y);
bool isGreateOrEqual = (x >= y);

//Logical Operators

bool condition1 = true;
bool condition2 = false;

bool andResult = condition1 && condition2;
bool orResult = condition1 || condition2;
bool notResult = !condition1;

//Assignment Operators

int number = 10;

number += 5; // result = 15
number -= 3; // result = 12
number *= 2; // result = 24
number /= 4; // result = 6
number %= 5; // result = 1

//Variable Declaration

//without initialization
int age;
string firstName;
bool isStudentActive;
double accountBalance;

//with initialization
int currentYear = 2024;
string companyName;
bool hasAccess = true;

//Initialization after declaration
int attemps;
attemps = 0;

//Multiple variable declaration
int width = 10, height = 20, depth = 5;

//Variable assignment
int counter = 0;
counter = counter + 1;

//Constant

const int MAX_STUDENTS = 10;
const double PI = 3.14159;
const string APPLICATION_NAME = "Student Management Systemn";

//Constant must be initialized at declaration

//Readonly variable

public class Configuration
{
    public readonly string ConnectionString;
    public readonly DateTime ApplicationStartTime = DateTime.Now;

    public Configuration(string ConnectionString)
    {
        ConnectionString = ConnectionString; //Valid in constuctor
    }
}

//Variable Scope

/*
Local Variables: Declare within methods, accessible only within that method
Instance Variables: Declared within classes, accessible throughout the class instance;
Static Variables: Shared across all instances of a class
*/

