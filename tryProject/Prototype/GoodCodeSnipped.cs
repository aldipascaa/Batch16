namespace prototype.goodexample;

public interface IShape{
    void Draw();
    IShape Duplicate();
}
public class Square: IShape
{
    public int side {get; set;} =5;
    public void Draw(){}
    public IShape Duplicate()
    {
        var newSquare = new Square();
        newSquare.side = side;
        return newSquare;
    }
}
public class Circle: IShape
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
public class Rectangle: IShape
{
    public int width {get; set;} =5;
    public int height {get; set;} =5;
    public void Draw(){}
    public IShape Duplicate()
    {
        var newRectangle = new Rectangle();
        newRectangle.width = width;
        newRectangle.height = height;
        return newRectangle;
    }
}
public class ShapeActions
{
    public IShape Duplicate(IShape shape)
    {
        return shape.Duplicate();
    }
}