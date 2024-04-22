namespace MyBoardsApp.Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string Message { get; set; }
    public string Author { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public int WorkItemId { get; set; }
    public WorkItem WorkItem { get; set; }
}