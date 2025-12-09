using System.Net.Http.Headers;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace AdvanceCSharp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("All in Advance C#");
        Delegates();
        EventHandler();
        TryStatementAndException();
        EnumerationAndIterators();
        NullableValue();
        OperatorOverloading();

    }

    // Delegates
    static void Delegates()
    {
        //1
        //Basic Delegate
        int result;
        int invokeResult;
        Transformer t = Square;
        result = t(3);
        invokeResult = t.Invoke(3);
        Console.WriteLine("Square through delegate:" + result);
        Console.WriteLine("Square through delegate:" + invokeResult);

        //2
        //Plugin Method Delegate
        int[] values = { 1, 2, 3, 4, 5 };
        Console.WriteLine("Original Values: " + string.Join(", ", values));
        //Transform array using Square plugin
        Transform(values, Square);
        Console.WriteLine("After Square Transform : " + string.Join(", ", values));
        //Lambda expression as a plugin
        values = new int[] { 1, 2, 3, 4, 5 };
        Transform(values, x => x + 10);
        Console.WriteLine($"After +10 transform: [{string.Join(", ", values)}]");

        //3
        //Instance and static method
        //static method-target
        Transformer staticDelegate = Square;
        Console.WriteLine("Static Square of 6:" + staticDelegate(6));
        //Instance method target
        Calculator calc = new Calculator(5);
        Transformer InstanceDelegate = calc.MultiplyBy;
        Console.WriteLine("Multiply 8 by " + calc.Multiplier + ": " + InstanceDelegate(8));
        // The delegate keeps the object alive - demonstrate this with Target property
        Console.WriteLine($"Delegate Target is null (static): {staticDelegate.Target == null}");
        Console.WriteLine($"Delegate Target is Calculator instance: {InstanceDelegate.Target is Calculator}");

        //4
        //Multicast Delegates
        //with non-void return types
        ProgressReporter reporter = WriteProgressToConsole;
        reporter += WriteProgressToFile;
        reporter += SendProgressAlert;
        Console.WriteLine("Progress reporting with multicast delegate (3 methods):");
        reporter(50);
        reporter(75);  // This calls ALL three methods in the order they were added
        Console.WriteLine("\nRemoving console reporter using -= operator:");

        //5
        //Generic Delegates
        Transformer<int, int> intSquarer = x => x * x;
        Transformer<string, int> stringLength = s => s.Length;
        Console.WriteLine($"Int squarer (5): {intSquarer(5)}");
        Console.WriteLine($"String length ('Hello'): {stringLength("Hello")}");
        // Using generic Transform method
        Console.WriteLine("\nGeneric Transform method demo:");
        int[] numbers = { 1, 2, 3, 4 };
        Console.WriteLine($"Original numbers: [{string.Join(", ", numbers)}]");
        TransformGeneric(numbers, x => x * x);  // Square each number
        Console.WriteLine($"Squared numbers: [{string.Join(", ", numbers)}]");
        string[] words = { "cat", "dog", "elephant" };
        Console.WriteLine($"Original words: [{string.Join(", ", words)}]");
        TransformGeneric(words, s => s.ToUpper());  // Uppercase each word
        Console.WriteLine($"Uppercase words: [{string.Join(", ", words)}]");
        Console.WriteLine();

        //6
        //Func and action
        Func<int, int> squareFunc = x => x * x;
        Func<int, int, int> addFunc = (a, b) => a + b;
        Func<string> getTimeFunc = () => DateTime.Now.ToString("HH:mm:ss");
        Console.WriteLine($"Func square of 7: {squareFunc(7)}");
        Console.WriteLine($"Func add 5 + 8: {addFunc(5, 8)}");
        Console.WriteLine($"Func current time: {getTimeFunc()}");
        Action simpleAction = () => Console.WriteLine("  Simple action executed");
        Action<string> messageAction = msg => Console.WriteLine($"  Message: {msg}");
        Action<int, string> complexAction = (num, text) =>
        Console.WriteLine($"  Number: {num}, Text: {text}");
        Console.WriteLine("Action demonstrations:");
        simpleAction();
        messageAction("Hello from Action!");
        complexAction(42, "The Answer");

        //7
        //Delegates vs Interfaces
        //Interface Approach
        Console.WriteLine("Interface approach");
        ITransformer squareTransformer = new SquareTransformer();
        ITransformer cubeTransformer = new CubeTransformer();
        TransformWithInterface(new int[] { 2, 3, 4 }, squareTransformer);
        TransformWithInterface(new int[] { 2, 3, 4 }, cubeTransformer);
        //Delegate approach
        Console.WriteLine("Delegate approach:");
        Func<int, int> squareDelegate = x => x * x;
        Func<int, int> cubeDelegate = x => x * x * x;
        TransformWithDelegate(new int[] { 2, 3, 4 }, squareDelegate);
        TransformWithDelegate(new int[] { 2, 3, 4 }, cubeDelegate);
        //One Class, multiple compatible methods
        Console.WriteLine("Multiple casting");
        MathOperations math = new MathOperations();
        TransformWithDelegate(new int[] { 2, 3, 4 }, math.Square);
        TransformWithDelegate(new int[] { 2, 3, 4 }, math.Cube);
        TransformWithDelegate(new int[] { 2, 3, 4 }, math.Double);

        //8
        //Delegate Compatibility
        void TestMethod() => Console.WriteLine("  Test method executed");
        D1 d1 = TestMethod;
        D2 d2 = new D2(d1); //explicit construction
        Console.WriteLine("Both delegates call the same method:");
        d1();
        d2();
        //multicast delegates
        D1 d1Multi = TestMethod;
        d1Multi += TestMethod;
        Console.WriteLine("Multicast");
        d1Multi();

        //9
        //Parameter Compatibility
        // Method that take general parameter type
        void ActOnObject(object obj) => Console.WriteLine($" Processing object: {obj}");
        // Delegate that expects a more specific parameter type
        Action<string> stringAction;
        stringAction = ActOnObject;
        stringAction("Hello contravariance");
        //Return type Compatibility
        string GetSpecificString() => "Hello from specific string method";
        Func<Object> objectGetter;
        objectGetter = GetSpecificString;
        object objResult = objectGetter;
        Console.WriteLine($"Returned: {objResult} (actual type: {objResult.GetType().Name})");
 
    }
#region Delegates support class
    delegate void D1();
    delegate void D2();
    // Interface approach
    interface ITransformer
    {
        int Transform(int x);
    }
    class SquareTransformer : ITransformer
    {
        public int Transform(int x) => x * x;
    }
    class CubeTransformer : ITransformer
    {
        public int Transform(int x) => x * x * x;
    }
    static void TransformWithInterface(int[] values, ITransformer transformer)
    {
        for (int i = 0; i < values.Length; i++)
            values[i] = transformer.Transform(values[i]);
        Console.WriteLine($"  Result: [{string.Join(", ", values)}]");
    }
    static void TransformWithDelegate(int[] values, Func<int, int> transformer)
    {
        for (int i = 0; i < values.Length; i++)
            values[i] = transformer(values[i]);
        Console.WriteLine($"  Result: [{string.Join(", ", values)}]");
    }
    // Single class with multiple methods (delegate advantage)
    class MathOperations
    {
        public int Square(int x) => x * x;
        public int Cube(int x) => x * x * x;
        public int Double(int x) => x * 2;
    }
    public static void TransformGeneric<T>(T[] values, Transformer<T,T> transformer)
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = transformer(values[i]);
        }
    }
    public delegate TResult Transformer<TArg,TResult>(TArg arg);
    delegate int Transformer(int x);
    static int Square(int x) => x * x;
    static int Cube(int x) => x * x * x;
    static void Transform(int[] values, Transformer t)
    {
        for (int i = 0; i < values.Length; i++)
            values[i] = t(values[i]);  // Apply the plugged-in transformation
    }
    public class Calculator
    {
        private int multiplier;

        public Calculator(int multiplier)
        {
            this.multiplier = multiplier;
        }

        public int Multiplier => multiplier;

        // Instance method that matches our Transformer delegate
        public int MultiplyBy(int input)
        {
            return input * multiplier;
        }
    }
    delegate void ProgressReporter(int percentComplete);
    static void WriteProgressToConsole(int percentComplete)
    {
        Console.WriteLine($"  Console Log: {percentComplete}% complete");
    }
    static void WriteProgressToFile(int percentComplete)
    {
        Console.WriteLine($"  File Log: Writing {percentComplete}% to progress.log");
    }
    static void SendProgressAlert(int percentComplete)
    {
        if (percentComplete >= 75)
            Console.WriteLine($"  Alert: High progress reached - {percentComplete}%!");
    }
#endregion
    //Event Handler
    static void EventHandler()
    {
        //1
        //Basic Declaration
        var priceMonitor = new BasicPriceMonitor("AALP");
        void Trader1Handler(decimal oldPrice, decimal newPrice) =>
            Console.WriteLine($"priceMonitor change from ${oldPrice} to ${newPrice}");
        priceMonitor.PriceChanged += Trader1Handler;
        priceMonitor.UpdatePrice(150.00m);

        //2
        //Event Delegate
        var eventPublisher = new EventPublisher();
        var delegatePublisher = new DelegatePublisher();
        //Subscribe to both
        eventPublisher.SafeNotification += msg => Console.WriteLine($"  Event received: {msg}");
        delegatePublisher.UnsafeNotification += msg => Console.WriteLine($"  Delegate received: {msg}");
        Console.WriteLine("Both subscribed successfully");
        eventPublisher.TriggerEvent("Hello from event");
        delegatePublisher.TriggerEvent("Hello from delegate");
        Console.WriteLine("\nTesting safety differences:");
        // Try to do dangerous things - these will show the difference
        // eventPublisher.SafeNotification = null;        // Compile error - can't assign to event
        // eventPublisher.SafeNotification("hack");       // Compile error - can't invoke from outside
        // But with delegate, these dangerous operations are possible:
        Console.WriteLine("Delegate allows dangerous operations:");
        delegatePublisher.UnsafeNotification("Hack!!");
        delegatePublisher.UnsafeNotification = null; // Wipes out all subscribers!
        delegatePublisher.TriggerEvent("This won't be received by anyone");
        Console.WriteLine("Event safety prevents subscriber interference\n");

        //3
        //standard event pattern
        //create using standard pattern
        var stock = new Stock("MSFT");
        var portfolio = new Portfolio("Tech Stocks");
        //subscribe using standard EventHandler<T> pattern
        stock.PriceChanged += portfolio.OnPriceChanged;
        stock.PriceChanged += (sender, e) =>
        {
            if (Math.Abs(e.PercentChange) > 5)
                Console.WriteLine($"  ALERT: Large price movement detected!");
        };
        //set initial price and change it
        stock.Price = 300.00m;
        stock.Price = 400.00m;

        //4
        //event accessors
        var smartNotifier = new SmartNotificationSystem();
        void Handler1(string msg) => Console.WriteLine($"Handler 1: {msg}");
        void Handler2(string msg) => Console.WriteLine($"Handler 2: {msg}");
        Console.WriteLine("Adding subcribers");
        smartNotifier.MessageReceived += Handler1;
        smartNotifier.MessageReceived += Handler2;
        smartNotifier.SendMessage("Hello everyone!!!");
        Console.WriteLine($"Total Subs: {smartNotifier.SubscriberCount}");

        //5
        //ThreadSafeEventHandling
        var processor = new DataProcessor();
        var logger = new EventLogger();
        //subscribe to events
        processor.DataProcessed += logger.LogDataProcessed;
        processor.DataProcessed += (sender, e) =>
            Console.WriteLine($" Processing complete: {e.ItemsProcessed} items in {e.Duration.TotalMilliseconds}ms");
        Console.WriteLine("Starting concurrent data processing...");
        //simulate multiple threads processing data
        var threads = new Thread[3];
        for (int i = 0 ; i < threads.Length; i++)
        {
            int threadId = i;
            threads[i] = new Thread(() =>
            {
                processor.ProcessData($"Dataset-{threadId}", 100 + threadId * 50);
            });
            threads[i].Start();
        }
        //wait for all threads to complete
        foreach (var thread in threads)
            thread.Join();
        Console.WriteLine("All processing completed safely");
    }
#region EventHandler
#region Basic Event Classes
    // Custom delegate for price changes - this defines the event signature
    public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);
// Basic broadcaster class - demonstrates fundamental event concepts
public class BasicPriceMonitor
{
    private decimal _currentPrice;
    
    public string Symbol { get; }

    // Event declaration - this is the key difference from a regular delegate field
    // Outside classes can only use += and -= on this event
    public event PriceChangedHandler? PriceChanged;

    public BasicPriceMonitor(string symbol)
    {
        Symbol = symbol;
        _currentPrice = 0;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (_currentPrice != newPrice)
        {
            decimal oldPrice = _currentPrice;
            _currentPrice = newPrice;

            // Inside the class, we can invoke the event like a regular delegate
            // This will call ALL subscribed methods in the order they were added
            PriceChanged?.Invoke(oldPrice, newPrice);
        }
    }
}
#endregion
#region Event vs Delegate Safety Demo
// Class using proper event (safe)
public class EventPublisher
{
    public event Action<string>? SafeNotification;

    public void TriggerEvent(string message)
    {
        Console.WriteLine($"Event publisher sending: {message}");
        SafeNotification?.Invoke(message);
    }
}
// Class using raw delegate (unsafe)
public class DelegatePublisher
{
    // This is just a public delegate field - dangerous!
    public Action<string>? UnsafeNotification;

    public void TriggerEvent(string message)
    {
        Console.WriteLine($"Delegate publisher sending: {message}");
        UnsafeNotification?.Invoke(message);
    }
}
#endregion
#region Standard Event Pattern Classes
// Proper EventArgs subclass following .NET conventions
public class PriceChangedEventArgs : EventArgs
{
    public string Symbol { get; }
    public decimal OldPrice { get; }
    public decimal NewPrice { get; }
    public decimal PriceChange => NewPrice - OldPrice;
    public decimal PercentChange => OldPrice == 0 ? 0 : (PriceChange / OldPrice) * 100;
    public DateTime Timestamp { get; }

    public PriceChangedEventArgs(string symbol, decimal oldPrice, decimal newPrice)
    {
        Symbol = symbol;
        OldPrice = oldPrice;
        NewPrice = newPrice;
        Timestamp = DateTime.Now;
    }
}
// Stock class implementing the standard .NET event pattern
public class Stock
{
    private decimal _price;

    public string Symbol { get; }
    public decimal Price
    {
        get => _price;
        set
        {
            if (_price != value)
            {
                decimal oldPrice = _price;
                _price = value;
                
                // Create proper EventArgs and fire the event
                OnPriceChanged(new PriceChangedEventArgs(Symbol, oldPrice, value));
            }
        }
    }

    // Standard event using EventHandler<T> - this is the preferred approach
    public event EventHandler<PriceChangedEventArgs>? PriceChanged;

    public Stock(string symbol)
    {
        Symbol = symbol;
    }

    // Protected virtual "On" method - this is standard .NET pattern
    // Allows derived classes to override event firing behavior
    protected virtual void OnPriceChanged(PriceChangedEventArgs e)
    {
        // Thread-safe event invocation using null-conditional operator
        // This prevents NullReferenceException if the last subscriber unsubscribes
        // between the null check and the invocation
        PriceChanged?.Invoke(this, e);
    }
}
// Subscriber class that handles stock price changes
public class Portfolio
{
    public string Name { get; }

    public Portfolio(string name)
    {
        Name = name;
    }

    // Event handler following standard pattern: object sender, EventArgs e
    public void OnPriceChanged(object? sender, PriceChangedEventArgs e)
    {
        var direction = e.PriceChange >= 0 ? "↑" : "↓";
        
        Console.WriteLine($"  Portfolio '{Name}': {e.Symbol} {direction} " +
                        $"${e.OldPrice:F2} → ${e.NewPrice:F2} " +
                        $"({e.PercentChange:+0.00;-0.00}%)");
    }
}
#endregion
#region Custom Event Accessors

// Class demonstrating explicit event accessors
public class SmartNotificationSystem
{
    // Private delegate field - the compiler normally generates this automatically
    private Action<string>? _messageReceived;
    private int _subscriberCount = 0;

    public int SubscriberCount => _subscriberCount;

    // Explicit event accessors - we control what happens during add/remove
    public event Action<string> MessageReceived
    {
        add
        {
            Console.WriteLine($"  Adding subscriber (current count: {_subscriberCount})");
            _messageReceived += value;
            _subscriberCount++;
        }
        remove
        {
            Console.WriteLine($"  Removing subscriber (current count: {_subscriberCount})");
            _messageReceived -= value;
            _subscriberCount--;
        }
    }

    public void SendMessage(string message)
    {
        Console.WriteLine($"Sending message: {message}");
        // Invoke the private delegate field directly
        _messageReceived?.Invoke(message);
    }
}
#endregion
#region Event Modifiers Demo
// Static events example
public static class SystemMonitor
{
    // Static event belongs to the type, not an instance
    public static event Action<string>? SystemAlert;

    public static void TriggerAlert(string alertMessage)
    {
        SystemAlert?.Invoke(alertMessage);
    }
}

// Base class with virtual event
public class BaseService
{
    // Virtual event can be overridden in derived classes
    public virtual event Action<object, string>? StatusChanged;

    public virtual void ChangeStatus(string status)
    {
        OnStatusChanged(status);
    }

    protected virtual void OnStatusChanged(string status)
    {
        StatusChanged?.Invoke(this, status);
    }
}

// Derived class overriding the virtual event
public class EnhancedService : BaseService
{
    // Override the virtual event with enhanced behavior
    public override event Action<object, string>? StatusChanged;

    protected override void OnStatusChanged(string status)
    {
        // Enhanced logging before firing the event
        Console.WriteLine($"  Enhanced service logging: Status changing to '{status}'");
        StatusChanged?.Invoke(this, $"[ENHANCED] {status}");
    }
}
#endregion
#region Thread-Safe Event Handling
public class DataProcessedEventArgs : EventArgs
{
    public string DatasetName { get; }
    public int ItemsProcessed { get; }
    public TimeSpan Duration { get; }
    public int ThreadId { get; }

    public DataProcessedEventArgs(string datasetName, int itemsProcessed, TimeSpan duration)
    {
        DatasetName = datasetName;
        ItemsProcessed = itemsProcessed;
        Duration = duration;
        ThreadId = Thread.CurrentThread.ManagedThreadId;
    }
}
public class DataProcessor
{
    // Thread-safe event declaration
    public event EventHandler<DataProcessedEventArgs>? DataProcessed;

    public void ProcessData(string datasetName, int itemCount)
    {
        var startTime = DateTime.Now;
        
        Console.WriteLine($"  Thread {Thread.CurrentThread.ManagedThreadId}: Processing {datasetName}...");
        
        // Simulate processing time
        Thread.Sleep(100 + new Random().Next(100));
        
        var duration = DateTime.Now - startTime;
        
        // Fire the event - this is thread-safe due to the null-conditional operator
        OnDataProcessed(new DataProcessedEventArgs(datasetName, itemCount, duration));
    }

    protected virtual void OnDataProcessed(DataProcessedEventArgs e)
    {
        // The ?. operator ensures thread safety even if subscribers change
        // between threads
        DataProcessed?.Invoke(this, e);
    }
}
public class EventLogger
{
    public void LogDataProcessed(object? sender, DataProcessedEventArgs e)
    {
        Console.WriteLine($"  Logger: {e.DatasetName} completed on thread {e.ThreadId} " +
                        $"({e.ItemsProcessed} items, {e.Duration.TotalMilliseconds:F0}ms)");
    }
}
#endregion
#region Real-World E-Commerce Classes
// Order data structure
public class CustomerOrder
{
    public int OrderId { get; }
    public string CustomerEmail { get; }
    public decimal Amount { get; }
    public string ProductName { get; }
    public DateTime OrderTime { get; }
    public CustomerOrder(int orderId, string customerEmail, decimal amount, string productName)
    {
        OrderId = orderId;
        CustomerEmail = customerEmail;
        Amount = amount;
        ProductName = productName;
        OrderTime = DateTime.Now;
    }
}
// EventArgs for order events
public class OrderEventArgs : EventArgs
{
    public CustomerOrder Order { get; }
    public DateTime EventTime { get; }

    public OrderEventArgs(CustomerOrder order)
    {
        Order = order;
        EventTime = DateTime.Now;
    }
}

// Main order processing system - the event broadcaster
public class OrderProcessingSystem
{
    // Multiple events for different business scenarios
    public event EventHandler<OrderEventArgs>? OrderPlaced;
    public event EventHandler<OrderEventArgs>? OrderCancelled;

    public void PlaceOrder(CustomerOrder order)
    {
        Console.WriteLine($"Order System: Processing order #{order.OrderId} for {order.CustomerEmail}");
        Console.WriteLine($"  Product: {order.ProductName}, Amount: ${order.Amount}");
        
        // Fire the event - all interested systems will be notified automatically
        OnOrderPlaced(new OrderEventArgs(order));
    }

    public void CancelOrder(CustomerOrder order)
    {
        Console.WriteLine($"Order System: Cancelling order #{order.OrderId}");
        
        OnOrderCancelled(new OrderEventArgs(order));
    }

    protected virtual void OnOrderPlaced(OrderEventArgs e)
    {
        OrderPlaced?.Invoke(this, e);
    }

    protected virtual void OnOrderCancelled(OrderEventArgs e)
    {
        OrderCancelled?.Invoke(this, e);
    }
}
// Email service - independent subscriber
public class EmailService
{
    public void OnOrderPlaced(object? sender, OrderEventArgs e)
    {
        Console.WriteLine($"  Email Service: Sending confirmation to {e.Order.CustomerEmail}");
        Console.WriteLine($"    'Your order #{e.Order.OrderId} has been confirmed'");
    }

    public void OnOrderCancelled(object? sender, OrderEventArgs e)
    {
        Console.WriteLine($"  Email Service: Sending cancellation notice to {e.Order.CustomerEmail}");
        Console.WriteLine($"    'Your order #{e.Order.OrderId} has been cancelled'");
    }
}
// Inventory system - another independent subscriber
public class InventorySystem
{
    public void OnOrderPlaced(object? sender, OrderEventArgs e)
    {
        Console.WriteLine($"  Inventory System: Reserving {e.Order.ProductName} for order #{e.Order.OrderId}");
        Console.WriteLine($"    Inventory levels updated");
    }

    public void OnOrderCancelled(object? sender, OrderEventArgs e)
    {
        Console.WriteLine($"  Inventory System: Releasing reserved {e.Order.ProductName}");
        Console.WriteLine($"    Item returned to available inventory");
    }
}
// Audit logging - tracks all order activities
public class AuditLogger
{
    public void OnOrderPlaced(object? sender, OrderEventArgs e)
    {
        Console.WriteLine($"  Audit Logger: ORDER_PLACED - ID:{e.Order.OrderId}, " +
                        $"Customer:{e.Order.CustomerEmail}, Amount:${e.Order.Amount}");
    }

    public void OnOrderCancelled(object? sender, OrderEventArgs e)
    {
        Console.WriteLine($"  Audit Logger: ORDER_CANCELLED - ID:{e.Order.OrderId}");
    }
}
// Loyalty program - calculates reward points
public class LoyaltyProgram
{
    public void OnOrderPlaced(object? sender, OrderEventArgs e)
    {
        int points = (int)(e.Order.Amount / 10); // 1 point per $10 spent
        Console.WriteLine($"  Loyalty Program: Adding {points} points to {e.Order.CustomerEmail}");
        Console.WriteLine($"    Customer rewards balance updated");
    }
}

    #endregion
    #endregion
    //Try Statement and exception
    static void TryStatementAndException()
    {
        //1
        // Basic Try and Catch
        try
        {
            // This line will throw a DivideByZeroException
            int result = Calc(0);
            Console.WriteLine($"Result: {result}"); // This line won't execute
        }
        catch (DivideByZeroException ex)
        {
            // Execution jumps here when the exception is thrown
            Console.WriteLine("✓ Caught DivideByZeroException - program continues running");
            Console.WriteLine($"  Exception message: {ex.Message}");
            Console.WriteLine($"  Exception type: {ex.GetType().Name}");
        }
        Console.WriteLine("✓ Program execution continues after exception handling\n");
        // Important principle: Prevention is better than exception handling
        Console.WriteLine("Better approach - validate input before risky operations:");
        int safeResult = SafeCalc(0);
        Console.WriteLine($"Safe result: {safeResult}");

        //2 
        // Multiple Catch Block
        // Simulate command line arguments for testing
        string[] testArgs = { "300" }; // Try: "300", "abc", "", or null
        Console.WriteLine($"Testing with argument: '{testArgs[0]}'");
        try
        {
            byte b = byte.Parse(testArgs[0]);
            Console.WriteLine($"Successfully parsed: {b}");
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Error: Please provide at least one argument");
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: That's not a valid number!");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Error: The number is too large to fit in a byte (max: 255)!");
        }
        catch (Exception ex) // General catch-all (should be last)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }

        // Test different scenarios
        Console.WriteLine("\nTesting different error scenarios:");
        TestParsingScenarios();
        Console.WriteLine();

        //3 
        // Exception Filter

        //4
        // Finally Block
        Console.WriteLine("Testing finally block execution:");
        // Test scenario 1: No exception
        Console.WriteLine("Scenario 1: Normal execution");
        TestFinallyBlock(false);
        // Test scenario 2: With exception
        Console.WriteLine("\nScenario 2: With exception");
        TestFinallyBlock(true);
        Console.WriteLine();

        //5 
        // Using Statement
        // Manual way
        Console.WriteLine("\nManual resource management:");
        ReadFileManually();
        // Using statement way
        Console.WriteLine("\nUsing Statement approach:");
        ReadFileWithUsing();

        //6
        // Using Declaration (C# 8)
        Console.WriteLine("Demonstrating using declaration syntax:");
        DemonstrateUsingDeclaration();

        //7
        // Throw Expression (C# 7)
        Console.WriteLine("Throw Expression");
        try
        {
            string result = GetNotImplementedFeature();
            Console.WriteLine(result);
        }
        catch (NotImplementedException ex)
        {
            Console.WriteLine($"  ✓ Caught from expression-bodied method: {ex.Message}");
        }
        // Test throw in ternary conditional
        try
        {
            string result = ProperCase(null);
            Console.WriteLine(result);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"  ✓ Caught from ternary expression: {ex.Message}");
        }

        // Test with valid input
        try
        {
            string result = ProperCase("hihow");
            Console.WriteLine($"  ✓ ProperCase result: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ✗ Unexpected error: {ex.Message}");
        }

        Console.WriteLine();

        //8
        // Common Exception Type

        //9
        // Argument Null Throw If Null (.NET 6)

        //10
        // Throwing Exceptions
        int number1 = int.Parse("123");
        //int number2 = int.Parse("abc"); //throws format exception


        //11
        // Rethrowing Exceptions

        //12
        // TryXXX Pattern
        int.TryParse("abc", out int result3);
        Console.WriteLine(result3);
        if (int.TryParse("123", out int result1))
        {
            Console.WriteLine($"Parsed: {result1}"); // Output: Parsed: 123
        }

        if (!int.TryParse("abc", out int result2))
        {
            Console.WriteLine($"Failed to parse {result2}"); // Output: Failed to parse 'abc'.
        }




        //13
        // Return Codes Alternative

    }
    #region Basic Try
    // Method that demonstrates what happens without input validation
    static int Calc(int x)
    {
        // This will throw DivideByZeroException if x is 0
        // In a real application, this represents any risky operation:
        // - File operations, network calls, parsing, etc.
        return 10 / x;
    }

    // Better approach - defensive programming with validation
    static int SafeCalc(int x)
    {
        // Always validate inputs when possible rather than relying on exception handling
        if (x == 0)
        {
            Console.WriteLine("  Warning: Division by zero attempted, returning safe value");
            return 0; // Or throw a more descriptive exception
        }
        return 10 / x;
    }
    #endregion
    #region Multiple catch
    static void TestParsingScenarios()
    {
        string[] testCases = { "100", "abc", "500", "" };

        foreach (string testCase in testCases)
        {
            Console.WriteLine($"  Testing '{testCase}':");
            try
            {
                byte result = byte.Parse(testCase);
                Console.WriteLine($"    Success: {result}");
            }
            catch (FormatException)
            {
                Console.WriteLine("    Error: Invalid format");
            }
            catch (OverflowException)
            {
                Console.WriteLine("    Error: Number too large for byte");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("    Error: Empty string");
            }
        }
    }

    #endregion
    #region Finally block

    static void TestFinallyBlock(bool throwException)
    {
        string? resource = null;

        try
        {
            Console.WriteLine("  Acquiring resource...");
            resource = "Important Resource";

            if (throwException)
            {
                throw new InvalidOperationException("Simulated error");
            }

            Console.WriteLine("  Using resource successfully");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"  Caught exception: {ex.Message}");
        }
        finally
        {
            // This ALWAYS runs, regardless of exceptions
            if (resource != null)
            {
                Console.WriteLine("  Finally block: Cleaning up resource");
                resource = null; // Simulate cleanup
            }
            else
            {
                Console.WriteLine("  Finally block: No resource to clean up");
            }
        }

        Console.WriteLine("  Method completed");
    }

    #endregion
    #region using statement approach
    static void ReadFileManually()
    {
        StreamWriter? writer = null;
        try
        {
            writer = new StreamWriter("manual_test.txt");
            writer.WriteLine("This file was created manually");
            Console.WriteLine("  File written successfully (manual way)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Error writing file: {ex.Message}");
        }
        finally
        {
            // We must remember to dispose manually
            if (writer != null)
            {
                writer.Dispose();
                Console.WriteLine("  Resource disposed manually in finally block");
            }
        }
    }
    static void ReadFileWithUsing()
    {
        try
        {
            // Using statement automatically calls Dispose() when exiting the block
            using (StreamWriter writer = new StreamWriter("using_test.txt"))
            {
                writer.WriteLine("This file was created with using statement");
                Console.WriteLine("  File written successfully (using statement)");
                // No need for manual cleanup - Dispose() is called automatically
            }
            Console.WriteLine("  Resource automatically disposed by using statement");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Error writing file: {ex.Message}");
        }
    }
    #endregion
    #region using Declarations
    static void DemonstrateUsingDeclaration()
    {
        // Create a temporary file for demonstration
        string tempFile = "using_declaration_demo.txt";

        try
        {
            if (File.Exists(tempFile))
            {
                // Using declaration - resource disposed when leaving the 'if' block
                using var reader = File.OpenText(tempFile);
                Console.WriteLine("  ✓ File opened with using declaration");
                string? firstLine = reader.ReadLine();
                Console.WriteLine($"  ✓ Read line: {firstLine ?? "empty"}");
                // reader.Dispose() is automatically called here when leaving scope
            }
            else
            {
                // Create the file first for demonstration
                using var writer = new StreamWriter(tempFile);
                writer.WriteLine("Demo content for using declaration");
                Console.WriteLine("  ✓ File created with using declaration");
                // writer.Dispose() is automatically called here
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ✗ Error: {ex.Message}");
        }
        finally
        {
            // Clean up the demo file
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
                Console.WriteLine("  ✓ Demo file cleaned up");
            }
        }
    }
    #endregion
    #region Throw Expression
    // Expression-bodied member that throws (C# 7+ throw expression)
    static string GetNotImplementedFeature() => 
        throw new NotImplementedException("This feature is planned for version 2.0");

    // Method using throw expression in ternary conditional
    static string ProperCase(string? value) =>
        value == null ? throw new ArgumentException("Value cannot be null") :
        value == "" ? "" :
        char.ToUpper(value[0]) + value.Substring(1).ToLower();

    #endregion
    //Enumeration and iterators
    static void EnumerationAndIterators()
    {
        List<int> list = [1, 2, 3];
        int[] list2 = [1, 2, 3];
        list.Add(10);
        list2.GetValue(0);
        Console.WriteLine("List Result :"+string.Join(",", list));
        Console.WriteLine("Array Result :"+list2.GetValue(0));
    }
    //Nullable Value
    static void NullableValue()
    {
        int regularInt = default;       // Results in 0, not absence of value
        bool regularBool = default;     // Results in false, not unknown state
        DateTime regularDate = default; // Results in 1/1/0001, not missing date

        // The following statement is invalid and will not compile:
        // int impossibleInt = null;  // Compile-time error
        string? someText = null;     // Valid - indicates no string object exists
        object? someObject = null;   // Valid - indicates no object reference
        int? nullableInt = null;        // Valid - represents absence of integer value
        bool? nullableBool = null;      // Valid - enables three-state logic
        DateTime? nullableDate = null;  // Valid - represents missing date information
        someText ??= "unknown";
        someObject ??= "unidentify";
        nullableInt ??= 0;
        nullableBool ??= false;
        nullableDate ??= default;

        Console.WriteLine($"default Int: {regularInt}");
        Console.WriteLine($"default Bool: {regularBool}");
        Console.WriteLine($"default DateTime: {regularDate}");
        // These declarations are functionally identical:
        int ? shorthandDeclaration = 42;
        Nullable<int> explicitDeclaration = new Nullable<int>(42);
        // Both create the same underlying structure with identical behavior
        Console.WriteLine(shorthandDeclaration.GetType() == explicitDeclaration.GetType()); // True
    }
    //Operator Overloading
    static void OperatorOverloading()
    {
        using (var enumerator = "beer".GetEnumerator())
{
    while (enumerator.MoveNext())
    {
        Console.WriteLine(enumerator.Current);
    }
}
    }
}

