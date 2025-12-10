namespace prototype.badexample;

public interface IShape{
    void Draw();
}
public class Square: IShape
{
    public int side {get; set;} = 5;
    public void Draw(){}
}
public class Circle: IShape
{
    public int radius {get; set;} =5;
    public void Draw(){}
}
public class Rectangle: IShape
{
    public int width {get; set;} = 5;
    public int height {get; set;} = 5;
    public void Draw(){}
}
public class ShapeActions
{
    public IShape Duplicate(IShape shape)
    {
        if(shape is Square)
        {
            var oldSquare = (Square)shape;
            var newSquare = new Square();
            newSquare.side = oldSquare.side;
            return newSquare;
        }
        else if(shape is Circle)
        {
            var oldCircle = (Circle)shape;
            var newCircle = new Circle();
            newCircle.radius = oldCircle.radius;
            return newCircle;
        }
        else if(shape is Rectangle)
        {
            var newRectangle = new Rectangle();
            var oldRectangle = (Rectangle)shape;
            newRectangle.width = oldRectangle.width;
            newRectangle.height = oldRectangle.height;
            return newRectangle;
        }
        else 
        {
            throw new ArgumentException("Invalid shape provided");
        }
    }
}