namespace MyBoardsApp.Entities;

public class Tag
{
    public int TagId { get; set; }
    public string Value { get; set; }

    public List<WorkItem> WorkItems { get; set; }
}