namespace ToDoList.Models;
public class Todo
{
    //Identity fields
    public int Id {get; set;}

    //Client-editable fields
    public string? Title {get; set;} = string.Empty;
    public bool IsCompleted {get; set;}

    //Server fields
    public int Priority {get; set;} = 0;
    public DateTime CreateAt {get; set;}
    public DateTime? UpdateAt {get; set;}
    public DateTime? CompletedAt {get; set;}
}
