namespace MyBoardsApp.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    
    public string Email { get; set; }

    public Address Address { get; set; }
    public List<WorkItem> WorkItems { get; set; } = new();
}