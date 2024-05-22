namespace MyBoardsApp.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    
    public string Email { get; set; }

    public virtual Address Address { get; set; }
    public virtual List<WorkItem> WorkItems { get; set; } = new();
    public virtual List<Comment> Comments { get; set; } = new();
}