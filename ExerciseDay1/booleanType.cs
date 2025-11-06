bool isValid = true;
bool uninitialized; //default to false
bool[] boolArray = new bool[5]; //default to false
Console.WriteLine(boolArray[0]);
bool flag = false;
int number;
//explicit conversion required

number = flag? 1 : 0;

Console.WriteLine(number);

bool isRaining;
bool isSunny;
bool condition1;
bool condition2;
bool condition3;

//Conditional (Tenary) Operator

// Acceptable nesting for simple logic
string weatherAdvice = isRaining ? "Take umbrella" : 
                      isSunny ? "Wear sunglasses" : 
                      "Check weather again";

// Complex nesting - consider using if-else for clarity
string complexResult = condition1 ? 
                      (condition2 ? "A" : "B") : 
                      (condition3 ? "C" : "D");

Console.WriteLine(weatherAdvice);
Console.WriteLine(complexResult);

