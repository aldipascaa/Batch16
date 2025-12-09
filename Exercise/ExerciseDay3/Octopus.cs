namespace Classes;
public class Octopus
{
    private string _name;
    public int Age = 2;
    public readonly Guid Id;
    public static readonly int Legs = 8;
    public static readonly int Eyes = 2;
    private static int _totalCreated = 0;
    public static int TotalCreated => _totalCreated;
    private bool _isHungry, _isSleeping, _isHiding;
    public Octopus(string name)
    {
        _name = name ?? "unnamed Octopus";
        Id = Guid.NewGuid();

        _isHungry = true;
        _isSleeping = false;
        _isHiding = false;

        _totalCreated++;

        Console.WriteLine($"New octopus born: {_name} (ID: {Id.ToString()[..8]}..., Total: {_totalCreated})");
    }
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public bool IsHungry
    {
        get { return _isHungry; }
        private set { _isHungry = value; }
    }
        public bool IsSleeping
    {
        get { return _isSleeping; }
        private set { _isSleeping = value; }
    }
    public void Feed()
    {
        if (_isHungry)
        {
            _isHungry = false;
            Thread.Sleep(3000);
            Console.WriteLine("...");
            Console.WriteLine($"{_name} is now well_fed and happy!");
        }
        else
        {
            Console.WriteLine($"{_name} is aleady full!");
        }
    }
    public void Hide()
    {
        _isHiding = !_isHiding;
        string status = _isHiding ? "hiding in a cave" : "coming out to play";
        Console.WriteLine(_name + "is now" + status);
    }
    public void Sleep()
    {
        _isSleeping = !_isSleeping;
        string status = _isSleeping ? "taking a nap" : "waking up";
        Console.WriteLine(_name + " is now " + status);
    }


}