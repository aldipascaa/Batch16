namespace prototype.example;
class Square: IShape
{
    public int side {get; set;} =5;
    public void Draw(){}
    public IShape Duplicate()
    {
        var newSquare = new Square();
        return newSquare;
    }
}