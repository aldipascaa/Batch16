namespace Classes;

public class Car
{
    private string _make;
    private string _model;
    private int _year;
    private double _mileage;

    public Car(string make) : this(make, "Unknown Model", DateTime.Now.Year)
    {
        //Console.WriteLine($"  🚗 Created basic car: {make}");
    }
    public Car(string make, string model) : this(make, model, DateTime.Now.Year)
    {
        //Console.WriteLine($"  🚗 Created car: {make} {model}");
    }
    public Car(string make, string model, int year)
    {
        _make = make ?? throw new ArgumentNullException(nameof(make));
        _model = model ?? "Unknown Model";
        _year = year;
        _mileage = 0.0;

        Console.WriteLine($"  🚗 Created detailed car: {year} {make} {model}");
    }
    public string Make => _make;
    public string Model => _model;
    public int Year => _year;
    public double Mileage => _mileage;
    public void Drive(double miles)
    {
        if(miles > 0)
        {}
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"  📋 {_year} {_make} {_model} - {_mileage:F1} miles");
    }
    public override string ToString()
    {
        return $"{_year} {_make} {_model} {_mileage:F1} miles";
    }
}
