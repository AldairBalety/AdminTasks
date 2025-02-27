namespace Backend.Models;
public class UserTask
{
    public int Id
    {
        get; set;
    }
    public string Title
    {
        get; set;
    }
    public string? Description
    {
        get; set;
    }
    public bool Completed
    {
        get; set;
    } = false;
    public DateTime? Created_at
    {
        get; set;
    }
    public DateTime? Updated_at
    {
        get; set;
    }
}