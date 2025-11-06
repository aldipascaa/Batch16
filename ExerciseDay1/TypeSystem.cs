/* 

Type system fundamentals

Type in C# is a blueprint that defines the structure behavior, 
and memory layout of data.

Type Safety:
-Compile-time Error Detection
-IntelliSense Support
-Performance Optimization
-Code Documentation

Type Hierarchy:
-Value Types
-Reference Types
-Special Types

Memory Allocation:
- Stack Memory: Fast, automatic, limited in size
- Heap Memory: Large, Managed by garbage collector

**Numeric Types:**
Numeric types are designed to handle mathematical operations and come in several categories based on the range of values they can represent and their precision requirements.

**Integer Types:**
- **Signed Integers**: Can represent positive and negative whole numbers
  - `sbyte`: 8-bit signed (-128 to 127)
  - `short`: 16-bit signed (-32,768 to 32,767)
  - `int`: 32-bit signed (-2,147,483,648 to 2,147,483,647) - most commonly used
  - `long`: 64-bit signed (very large range for big numbers)

- **Unsigned Integers**: Can only represent positive whole numbers but with larger positive range
  - `byte`: 8-bit unsigned (0 to 255)
  - `ushort`: 16-bit unsigned (0 to 65,535)
  - `uint`: 32-bit unsigned (0 to 4,294,967,295)
  - `ulong`: 64-bit unsigned (0 to very large positive numbers)

**Floating-Point Types:**
- `float`: 32-bit single precision, approximately 7 decimal digits of precision
- `double`: 64-bit double precision, approximately 15-17 decimal digits of precision
- `decimal`: 128-bit high precision, 28-29 decimal digits, designed for financial calculations

**Text Types:**
- `char`: Represents a single Unicode character (16-bit)
- `string`: Represents an immutable sequence of Unicode characters

**Boolean Type:**
- `bool`: Represents logical values, can only be `true` or `false`

**Default Values:**
Every type in C# has a default value that is automatically assigned when a variable is declared but not explicitly initialized:
- Numeric types default to `0`
- `bool` defaults to `false`
- `char` defaults to `\0` (null character)
- Reference types default to `null`
- `DateTime` defaults to `01/01/0001 00:00:00`

Understanding default values is crucial for avoiding unexpected behavior in applications, especially when working with arrays or class fields that are not explicitly initialized.

*/

byte smallNumber = 255; //8-bit unsigned: 0 to 255
sbyte signedSmall = -128; //8-bit signed: -128 to 127
short shortnumber = 32767; //16-bit signed -32,768 to 32,767
ushort unsignedShort = 65535; //16-bit unsigned 0 to 65,535
int number = 2147483647;          // 32-bit signed: -2.1B to 2.1B
uint unsignedNumber = 4294967295; // 32-bit unsigned: 0 to 4.3B
long bigNumber = 9223372036854775807;     // 64-bit signed
ulong unsignedBig = 18446744073709551615; // 64-bit unsigned

// Floating-point types
float precision = 3.14159f;       // 32-bit: ~7 digits precision
double doublePrecision = 3.141592653589793; // 64-bit: ~15-17 digits
decimal money = 123.456789m;      // 128-bit: 28-29 digits (financial)

// Character and text types
char letter = 'A';                // Single Unicode character
string text = "Hello, World!";    // Immutable string of characters

// Boolean type
bool isActive = true;             // true or false only

// Special types
object anything = "Can hold any type"; // Universal base type
dynamic runtime = "Resolved at runtime"; // Dynamic typing

//Type Conversion

//Implicit conversions
int intValue = 42;
long longValue = intValue;
double doubleValue = intValue;

//Explicit conversions
double sourceDouble = 123.456;
int truncated = (int)sourceDouble;

//Parsing from string
string numbertext = "42";
int parsed = int.Parse(numbertext);
bool success = int.TryParse("invalid", out parsed);

public class Calculator
{
    // Instance field - each Calculator object has its own
    private double _lastResult;
    
    // Static field - shared by all Calculator instances
    private static int _calculationCount;
    
    // Instance property
    public double LastResult => _lastResult;
    
    // Static property
    public static int TotalCalculations => _calculationCount;
    
    // Instance method - operates on specific object
    public double Add(double a, double b)
    {
        _lastResult = a + b;
        _calculationCount++; // Can access static members
        return _lastResult;
    }
    
    // Static method - operates on class level
    public static double Multiply(double a, double b)
    {
        _calculationCount++; // Can only access static members
        return a * b;
    }
    
    // Static constructor - runs once when class is first used
    static Calculator()
    {
        _calculationCount = 0;
        Console.WriteLine("Calculator class initialized");
    }
}

// Usage demonstration
Calculator calc1 = new Calculator();
Calculator calc2 = new Calculator();

double result1 = calc1.Add(5, 3);        // Instance method call
double result2 = Calculator.Multiply(4, 2); // Static method call

Console.WriteLine($"Calc1 last result: {calc1.LastResult}"); // 8
Console.WriteLine($"Calc2 last result: {calc2.LastResult}"); // 0
Console.WriteLine($"Total calculations: {Calculator.TotalCalculations}"); // 2