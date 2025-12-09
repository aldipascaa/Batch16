namespace prototype.example;

public class Rectangle : IShape
{
    public int width { get; set; } = 5;
    public int height { get; set; } = 5;

    public void Draw() { }
    public IShape Duplicate()
    {
        var newRectangle = new Rectangle();
        newRectangle.width = width;
        newRectangle.height = height;
        return newRectangle;
    }
}