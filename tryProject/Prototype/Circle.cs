namespace prototype.example;

class Circle: IShape
{
    public int radius {get; set;} =5;
    public void Draw(){}
    public IShape Duplicate()
    {
        var newCircle = new Circle();
        newCircle.radius = radius;
        return newCircle;
    }
}